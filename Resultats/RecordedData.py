import glob
import numpy as np

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
            data[user_id]["params"] = []
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
                params = lines[3][13:].split(",")
                bullet_times = lines[4][13:].split()

                data[user_id]["bpm"].append(bpm)
                data[user_id]["face_detected"].append(face_detected)
                data[user_id]["params"].append(params)
                data[user_id]["bullet_time"].append(bullet_times)

        self.data = data


    def get_bt_neighbor_means(self, length):
        prevs = []
        posts = []
        for user in self.data:
            for i in range(len(self.data[user]["bullet_time"])):
                centers = self.data[user]["bullet_time"][i]

                for center in centers:
                    center = int(center)
                    first = max(0, center-length)
                    prev = [int(value) for value in self.data[user]["bpm"][i][first:center]]
                    post = [int(value) for value in self.data[user]["bpm"][i][center+1:center+length+1]]

                    if(len(prev) == 0 or len(post) == 0):
                        continue

                    prevs.append(np.mean(prev))
                    posts.append(np.mean(post))
        return prevs, posts

    def avg_bt(self):
        tasks = [[], [], [], []]
        for user in self.data:
            for i in range(len(self.data[user]["bullet_time"])):
                bullet_times = self.data[user]["bullet_time"][i]
                tasks[RecordedData.params_to_task_id(self.data[user]["params"][i])].append(len(bullet_times))

        means = [np.mean(t) for t in tasks]
        stds = [np.std(t) for t in tasks]
        return means, stds


    def avg_bpm(self):
        tasks = [[], [], [], []]
        for user in self.data:
            for i in range(len(self.data[user]["bpm"])):
                bpm = [int(e) for e in self.data[user]["bpm"][i]]
                tasks[RecordedData.params_to_task_id(self.data[user]["params"][i])].append(np.mean(bpm))

        means = [np.mean(t) for t in tasks]
        stds = [np.std(t) for t in tasks]
        return means, stds

    def avg_bpm_over_time(self):
        tasks = [[], [], [], []]
        for user in self.data:
            for i in range(len(self.data[user]["bpm"])):
                bpm = [int(e) for e in self.data[user]["bpm"][i]]
                tasks[RecordedData.params_to_task_id(self.data[user]["params"][i])].append(bpm)

        return tasks


    def params_to_task_id(params):
        return (params[1] == "HexaOn") + ((params[2] == "BGon") * 2)
