import numpy as np
from process_nogui import Process
from webcam import Webcam

class NOGUI:
    def __init__(self):
        self.webcam = Webcam()
        self.input = self.webcam
        print("Input: webcam")
        self.process = Process()
        self.status = False
        self.frame = np.zeros((10,10,3),np.uint8)
        self.bpm = 0

    def selectInput(self):
        self.reset()
        self.input = self.webcam
        print("Input: webcam")

    def reset(self):
        self.process.reset()

    def main_loop(self):
        frame = self.input.get_frame()

        self.process.frame_in = frame
        is_face_detected = self.process.run()
        face_msg = ""
        if(not is_face_detected): face_msg = "face undetected"

        self.bpm = self.process.bpm #get the bpm change over the time
        print("bpm:", self.bpm, face_msg)

    def run(self, input):
        self.reset()
        input = self.input
        if self.status == False:
            self.status = True
            input.start()
            # self.btnStart.setText("Stop")
            while self.status == True:
                self.main_loop()
        elif self.status == True:
            self.status = False
            input.stop()
            # self.btnStart.setText("Start")

if __name__ == '__main__':
    ex = NOGUI()
    ex.run(ex.input)
