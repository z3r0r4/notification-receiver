#TODO send reply answer json to android
#TODO maybe switch to c++ or c# for server cus windows  
#TODO store notifications
#TODO dissmiss notifications

#Integrated with windows
#TODO frontend: display active Notifications
#TODO frontend: add reply Box
from rich.console import Console
console = Console()

class MirrorNotification:
    
    # appName #string
    # id
    # isCancel 
    # #boo
    # key 
    # text
    # ticker
    # time
    # title
    def notify(self):
        print("New Notification:")
        console.log(self.JsonNotification)

    def __str__(self):
        return str(self.__dict__)

    def __init__(self, StringNotification): 
        self.JsonNotification = json.loads(StringNotification)  
        try:
            self.appName = str(self.JsonNotification["appName"])
            self.id = str(self.JsonNotification["id"])
            self.isCancel = str(self.JsonNotification["isCancel"])
            self.key = str(self.JsonNotification["key"])
            self.text = str(self.JsonNotification["text"])
            self.ticker = str(self.JsonNotification["ticker"])
            self.time = str(self.JsonNotification["time"])
            self.title = str(self.JsonNotification["title"])
        except KeyError:
            print("missing some key")

import socket

TCP_IP = '192.168.178.84'
TCP_PORT = 9001
BUFFER_SIZE = 1024
reply = b'Test'

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.bind((TCP_IP, TCP_PORT))
s.settimeout(10)
s.listen(3) #allow 3 connections (client threads)
# conn, addr = s.accept()

import json
# import winsound
# frequency = 2000  # Set Frequency To 2500 Hertz
# duration = 100  # Set Duration To 1000 ms == 1 second

# print("Connection address:", addr)
while 1:
    try:
        conn, addr = s.accept()
        data = conn.recv(BUFFER_SIZE) #returns "" after close https://stackoverflow.com/questions/16745409/what-does-pythons-socket-recv-return-for-non-blocking-sockets-if-no-data-is-r 
        
        mn = MirrorNotification(data)

        print('\a') # winsound.Beep(frequency, duration)
        print("received data:\n", data)
        # print("extracted:\n", mn)
        mn.notify()

        if data == b'':
            raise RuntimeError("socket connection broken")
        # if not data:
        #     input("Press Enter to reply...")
        #     print("replying data", reply)
        #     conn.sendall(reply)
        #     break
    except KeyboardInterrupt:
        print('Interrupted')
        raise RuntimeError("Interrupted")
        
conn.close()



# import socketserver

# class Handler_TCPServer(socketserver.BaseRequestHandler):
#     """
#     The TCP Server class for demonstration.

#     Note: We need to implement the Handle method to exchange data
#     with TCP client.

#     """

#     def handle(self):
#         # self.request - TCP socket connected to the client
#         self.data = self.request.recv(8192).strip()
#         print("{} sent:".format(self.client_address[0]))
#         print(self.data)
#         # just send back ACK for data arrival confirmation
#         self.request.sendall("ACK from TCP Server".encode())

# if __name__ == "__main__":
#     HOST, PORT = "192.168.178.10", 9001

#     # Init the TCP server object, bind it to the localhost on 9999 port
#     tcp_server = socketserver.TCPServer((HOST, PORT), Handler_TCPServer)
#     print("Maybe")
#     # Activate the TCP server.
#     # To abort the TCP server, press Ctrl-C.
#     tcp_server.serve_forever()
