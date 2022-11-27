import random

f = open('birds.txt', 'w')
x, y, z = 300, 300, 300
random.seed(42)

taken: set[tuple[int, int, int]] = set()
cnt = 0
while cnt < 2000:
    rx = random.randrange(0, x)
    ry = random.randrange(0, y)
    rz = random.randrange(0, z)
    if (rx, ry, rz) in taken:
        continue
    f.write(f'        {{ "X": {rx}, "Y": {ry}, "Z": {rz}, "Behaviour": "behaviour 1" }},\n')
    taken.add((rx, ry, rz))
    cnt += 1

f.close()
