import numpy as np
from process_nogui import Process
from webcam import Webcam
import zmq

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
        face_msg = "T"
        if(not is_face_detected): face_msg = "F"

        self.bpm = self.process.bpm #get the bpm change over the time
        # print("bpm:", self.bpm, face_msg)
        socket.send_string(face_msg + ";" + str(self.bpm))
        message = socket.recv()
        print("Received reply %s %s [ %s ]" % (face_msg, self.bpm, message))

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
    context = zmq.Context()
    #  Socket to talk to server
    # print("Connecting to hello world server…")
    socket = context.socket(zmq.REQ)
    socket.connect("tcp://127.0.0.1:9701")

    ex = NOGUI()
    ex.run(ex.input)
