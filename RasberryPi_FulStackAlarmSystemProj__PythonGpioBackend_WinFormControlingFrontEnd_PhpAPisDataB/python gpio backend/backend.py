from gpiozero import Button, LED
import time
import requests
import threading


class Alarm:

    def send_state_to_app(self, app_url):
        params = {'state': self.state, 'zone1': self.zone1, 'zone2': self.zone2, 'zone3': self.zone3}
        r = requests.post(app_url, params=params)
        if r.status_code != 200:
            print(f'Failed to send state to app. Status code: {r.status_code}, Response: {r.text}')
        else:
            response_json = r.json()
            print(f"Response from app: {response_json}")

    def fetch_data_from_db(self):
        try:
            response = requests.get("http://172.20.10.2/alarmProject/fromDBtoPI.php") # Modifiez l'URL ici
            if response.status_code == 200:
                return response.json()
            else:
                print(f"Error fetching data from database: {response.status_code}")
                return None
        except Exception as e:
            print(f"Error fetching data from database: {e}")
            return None




    def check_for_data_changes(self, data):
        state = int(data["state"])
        zone1 = int(data["zone1"])
        zone2 = int(data["zone2"])
        zone3 = int(data["zone3"])

        if state != self.state or zone1 != self.zone1 or zone2 != self.zone2 or zone3 != self.zone3:
            self.prev_state = self.state  # Mettez à jour l'état précédent
            self.state = state
            self.zone1 = zone1
            self.zone2 = zone2
            self.zone3 = zone3
            self.update_7segment_display_based_on_data()

    def show_sequence(self):
        if self.state == 1:
            self.show1()
            time.sleep(1)
            self.show2()
            time.sleep(1)
            self.show3()
            time.sleep(1)
            self.showA()


    def update_7segment_display_based_on_data(self):
        if self.state == 1:
            if self.first_run:
                self.first_run = False  # Réinitialisez la valeur de first_run
                if self.zone1:
                    self.show1()
                elif self.zone2:
                    self.show2()
                elif self.zone3:
                    self.show3()
                else:
                    self.showA()
            elif self.prev_state == 0:  # Si l'état précédent était OFF
                self.show_sequence()
            elif self.zone1:
                self.show1()
            elif self.zone2:
                self.show2()
            elif self.zone3:
                self.show3()
            else:
                self.showA()
        else:
            self.hideAll()


    def run_update_7segment_display_loop(self):
        while True:
            data = self.fetch_data_from_db()
            if data:
                self.check_for_data_changes(data)
            time.sleep(3)


    def On_OFF_Toggle(self):
        new_state = 0 if self.state == 1 else 1
        if new_state == 0:
            self.zone1 = False
            self.zone2 = False
            self.zone3 = False
        params = {'state': new_state, 'zone1': self.zone1, 'zone2': self.zone2, 'zone3': self.zone3}
        r = requests.post(self.link, params=params)
        if r.status_code != 200:
            print(f"Failed to toggle ON/OFF. Status code: {r.status_code}, Response: {r.text}")
        else:
            response_json = r.json()
            self.prev_state = self.state
            self.state = new_state
            if self.state == 1:
                self.show_sequence()
            else:
                self.hideAll()


    def show1toA_1SecondeBetween(self):
        self.show1()
        time.sleep(1)
        self.show2()
        time.sleep(1)
        self.show3()
        time.sleep(1)
        self.showA()

    def showA(self):
        if (self.state ==1):
            self.a.off()
            self.b.off()
            self.c.off()
            self.d.on()
            self.e.off()
            self.f.off()
            self.g.off()
            self.pd.on()

    def show1(self):
        if (self.state ==1):
            self.hideAll()
            self.a.on()
            self.b.off()
            self.c.off()
            self.d.on()
            self.e.on()
            self.f.on()
            self.g.on()
            self.pd.on()



    def show2(self):
        if (self.state ==1):
            self.hideAll()
            self.a.off()
            self.b.off()
            self.c.on()
            self.d.off()
            self.e.off()
            self.f.on()
            self.g.off()
            self.pd.on()



    def show3(self):
        if (self.state ==1):
            self.hideAll()
            self.a.off()
            self.b.off()
            self.c.off()
            self.d.off()
            self.e.on()
            self.f.on()
            self.g.off()
            self.pd.on()


    def hideAll(self):
        self.a.on()
        self.b.on()
        self.c.on()
        self.d.on()
        self.e.on()
        self.f.on()
        self.g.on()
        self.pd.on()
        self.valve.off()


    def update_zones(self, z1, z2, z3):
        if (self.state ==1):
            self.zone1 = z1
            self.zone2 = z2
            self.zone3 = z3
            self.send_state_to_app(self.link)


    def __init__(self):

        self.link = "http://172.20.10.2/alarmProject/fromPiToDB.php"

        self.first_run = True

        # Button (Capteur d'eau)
        self.prev_state = 0
        self.cn0 = Button(23)
        self.cn1 = Button(18)
        self.cn2 = Button(15)
        self.cn3 = Button(14)

        # LED (Afficheur)===
        self.a = LED(24)
        self.b = LED(25)
        self.c = LED(8)
        self.d = LED(7)
        self.e = LED(1)
        self.f = LED(12)
        self.g = LED(16)
        self.pd = LED(20)

        # Valve
        self.valve = LED(21)

        # counter On/OFF
        self.state = 0

        # Zones
        self.zone1 = False
        self.zone2 = False
        self.zone3 = False

        # Button Listners
        self.cn0.when_pressed = self.On_OFF_Toggle

        self.cn1.when_pressed = self.show1
        self.cn1.when_released = lambda: self.update_zones(True, False, False)

        self.cn2.when_pressed = self.show2
        self.cn2.when_released = lambda: self.update_zones(False, True, False)

        self.cn3.when_pressed = self.show3
        self.cn3.when_released = lambda: self.update_zones(False, False, True)

        update_thread = threading.Thread(target=self.run_update_7segment_display_loop)
        update_thread.daemon = True
        update_thread.start()





alarm = Alarm()
alarm.hideAll()
