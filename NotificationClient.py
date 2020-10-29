# import socket, time


# TCP_IP = '127.0.0.1'
# TCP_PORT = 5005
# BUFFER_SIZE = 1024
# MESSAGE = "Hello, World!"

# s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
# s.connect((TCP_IP, TCP_PORT))
# s.send(MESSAGE)
# data = s.recv(BUFFER_SIZE)
# print("received data:", data)

# s.close()


# time.sleep(10)

import socket

host_ip, server_port = "192.168.178.10", 9999
data = " Hello how are you?\n"

# Initialize a TCP client socket using SOCK_STREAM
tcp_client = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

try:
    # Establish connection to TCP server and exchange data
    tcp_client.connect((host_ip, server_port))
    tcp_client.sendall(data.encode())

    # Read data from the TCP server and close the connection
    received = tcp_client.recv(1024)
finally:
    tcp_client.close()

print ("Bytes Sent:     {}".format(data))
print ("Bytes Received: {}".format(received.decode()))

# from pythonping import ping
# ping('192.168.178.49', verbose=True)

