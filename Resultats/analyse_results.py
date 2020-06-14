# import glob
#
# datas = {}
#
# for file in glob.glob("Mesures/log_user*"):
#
#     # Extract user id from filename if the naming convention is constant for
#     # all files
#     user_id = file[16:-4]
#     datas[user_id] = {}
#     datas[user_id]["bpm"] = []
#     datas[user_id]["face_detected"] = []
#     datas[user_id]["bullet_time"] = []
#
#     with open(file) as f:
#         content = f.read()
#
#     games = content.split("\n\n")
#     for game in games[-4:]: # Only check the last 4 recorded games of the file
#         lines = game.split("\n")
#         lines = [l for l in lines if l]
#
#         # Extract the infos that interest us
#         bpm = lines[0][13:].split()
#         face_detected = [value != "0" for value in lines[1][13:].split()]
#         bullet_times = lines[4][13:].split()
#
#         datas[user_id]["bpm"].append(bpm)
#         datas[user_id]["face_detected"].append(face_detected)
#         datas[user_id]["bullet_time"].append(bullet_times)
#
#
# for key in datas:
#     print(datas[key], end="\n\n")

from RecordedData import RecordedData as rd

data = rd("Mesures/log_user*")

print(data.data)

data.get_bullet_time_neighbor(5)
