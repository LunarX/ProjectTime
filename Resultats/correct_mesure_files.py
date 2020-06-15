import glob

for file in glob.glob("Mesures/log_user*"):

    with open(file) as f:
        lines = f.readlines()

    with open(file[:-4] + "_corrected" + ".txt", "w+") as f:
        for line in lines:
            # If the line has only one element after "BPM-Detect : "
            if("BPM-Detect" in line and len(line[13:].split()) == 1):
                # Then correct the format
                newline = [value for value in line[13:].split()[0]]
                f.write("BPM-Detect : " + " ".join(newline) + "\n")

            # Else copy the line as it is
            else:
                f.write(line)
