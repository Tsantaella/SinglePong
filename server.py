import socket

HOST = '127.0.0.1'
PORT = 65432

with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
    s.bind((HOST, PORT))
    print("binded...")
    s.listen()
    print("listening...")
    conn, addr = s.accept()
    with conn:
        try:
            while True:
                print('Connected by', addr)
                while True:
                    data = conn.recv(65507)
                    if not data:
                        break
                    print(data)
                    conn.sendall(b"adeu")
        except KeyboardInterrupt:
            pass