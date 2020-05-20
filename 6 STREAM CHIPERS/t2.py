import time


def RC4crypt(data, key):
    x = 0
    box = list(range(256))
    for i in list(range(256)):
        x = (x + box[i] + ord(key[i % len(key)])) % 256
        box[i], box[x] = box[x], box[i]
    x = y = 0
    out = []
    for char in data:
        x = (x + 1) % 256
        y = (y + box[x]) % 256
        box[x], box[y] = box[y], box[x]
        out.append(chr(ord(char) ^ box[(box[x] + box[y]) % 256]))
    return ''.join(out)


def hexRC4crypt(data, key):
    dig = RC4crypt(data, key)
    tempstr = ''
    for d in dig:
        xxx = '%02x' % (ord(d))
        tempstr = tempstr + xxx
    return tempstr

start_time = time.time()
hexrc = hexRC4crypt('Agapkina Diana Sergeevna', '4345100211')
print("--- %s seconds ---" % (time.time() - start_time))

print(hexrc)