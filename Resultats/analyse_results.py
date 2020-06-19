import matplotlib.pyplot as plt
import numpy as np
import csv

from RecordedData import RecordedData as rd

def plot_mean_std(mean, std):
    barWidth = 0.3
    r1 = np.arange(len(mean)) - (barWidth / 2)
    r2 = [x + barWidth for x in r1]

    plt.bar(r1, mean, width = barWidth, color = 'blue', edgecolor = 'black', label='mean')
    plt.bar(r2, std, width = barWidth, color = 'cyan', edgecolor = 'black', label='std')


data = rd("Mesures/log_user*")


# Hausse du pouls après bullet time
prev, post = data.get_bt_neighbor_means(8)
diff = np.array(post) - np.array(prev)
mean = np.mean(diff)
std = np.std(diff)

print("Hausse du pouls après bullet time:")
print("mean:", "{0:.2f}".format(mean), " std:", "{0:.2f}".format(std), "\n")



# Moyenne du nombre de bullet time déclanché par tâche
mean, std = data.avg_bt()

plt.figure()
plot_mean_std(mean, std)

plt.xticks(range(4), ['Avec rien', 'Avec hexa', 'Avec indicateurs', 'Avec tout'])
plt.xlabel("Tâche")
plt.ylabel("Moyenne sur tous les utilisateurs")
plt.title("Moyenne du nombre de bullet time déclanché par tâche")
plt.legend()


# BPM moyen par tâche
mean, std = data.avg_bpm()

plt.figure()
plot_mean_std(mean, std)

plt.xticks(range(4), ['Avec rien', 'Avec hexa', 'Avec indicateurs', 'Avec tout'])
plt.xlabel("Tâche")
plt.ylabel("Moyenne sur tous les utilisateurs")
plt.title("BPM moyen par tâche")
plt.legend()



# Evolution du BPM par tâche
tasks = data.avg_bpm_over_time()


for bpms in tasks:
    plt.figure()
    for bpm in bpms:
        x = np.linspace(0, 1, len(bpm))
        plt.plot(x, bpm, '-r')

    plt.xlabel("Durée de la partie")
    plt.ylabel("Bpm")




# Questionaire
with open("Mesures\Questionnaire TimeRythm.csv") as csv_file:
    csv_reader = csv.reader(csv_file, delimiter=',')
    tasks = [[], [], [], []]
    line_counter = 0
    for row in csv_reader:

        if(line_counter == 0):
            line_counter += 1
            continue

        tasks[0].append(row[17])
        tasks[1].append(row[18])
        tasks[2].append(row[19])
        tasks[3].append(row[20])

    means = []
    stds = []
    for task in tasks:
        t = [int(t) for t in task]

        mean = np.mean(t)
        std = np.std(t)

        means.append(mean)
        stds.append(std)

plt.figure()
plot_mean_std(means, stds)

plt.xticks(range(4), ['Avec rien', 'Avec hexa', 'Avec indicateurs', 'Avec tout'])
plt.xlabel("Tâche")
plt.ylabel("Note donnée par les utilisateurs de 1 à 5")
plt.title("Stresse ressentit par tâche selon le questionaire")
plt.legend()



plt.show()
