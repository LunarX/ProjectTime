import glob

class RecordedData:
    def __init__(self, data_path):
        data = {}

        for file in glob.glob("Mesures/log_user*"):

            # Extract user id from filename if the naming convention is constant for
            # all files
            user_id = file[16:-4]
            data[user_id] = {}
            data[user_id]["bpm"] = []
            data[user_id]["face_detected"] = []
            data[user_id]["bullet_time"] = []

            with open(file) as f:
                content = f.read()

            games = content.split("\n\n")
            for game in games[-4:]: # Only check the last 4 recorded games of the file
                lines = game.split("\n")
                lines = [l for l in lines if l]

                # Extract the infos that interest us
                bpm = lines[0][13:].split()
                face_detected = [value != "0" for value in lines[1][13:].split()]
                bullet_times = lines[4][13:].split()

                data[user_id]["bpm"].append(bpm)
                data[user_id]["face_detected"].append(face_detected)
                data[user_id]["bullet_time"].append(bullet_times)

        self.data = data


    def get_bullet_time_neighbor(self, length):
        for user in self.data:
            for i in range(len(self.data[user]["bullet_time"])):
                centers = self.data[user]["bullet_time"][i]

                for center in centers:
                    center = int(center)
                    prev = [int(value) for value in self.data[user]["bpm"][i][center-length:center]]
                    post = [int(value) for value in self.data[user]["bpm"][i][center+1:center+length+1]]

                    print("prev:", prev)
                    print("post:", post)
                    print("")
