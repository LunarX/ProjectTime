import matplotlib.pyplot as plt

from RecordedData import RecordedData as rd


data = rd("Mesures/log_user*")

# data.get_bt_neighbor(5)

averages = data.avg_bt()

plt.bar(range(4), averages)
plt.xticks(range(4), ['Avec rien', 'Avec hexa', 'Avec indicateurs', 'Avec tout'])
plt.xlabel("Tâche")
plt.ylabel("Moyenne sur tous les utilisateurs")
plt.title("Moyenne du nombre de bullet time déclanché par tâche")
plt.show()
