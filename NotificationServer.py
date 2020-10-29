# import socket

# TCP_IP = '192.168.178.10'
# TCP_PORT = 9999
# BUFFER_SIZE = 74

# s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
# s.bind((TCP_IP, TCP_PORT))
# s.listen(1)
# conn, addr = s.accept()

# print("Connection address:", addr)
# while 1:
#     data = conn.recv(BUFFER_SIZE)
#     if not data: break
#     print("received data", data)
#     #conn.send(data)
# conn.close()


#TODO send reply answer json to android
#TODO maybe switch to c++ or c# for server cus windows  
#TODO store notifications
#TODO dissmiss notifications
#TODO answer notifications

#Integrated with windows
#TODO frontend: display active Notifications
#TODO frontend: add reply Box


import socketserver

class Handler_TCPServer(socketserver.BaseRequestHandler):
    """
    The TCP Server class for demonstration.

    Note: We need to implement the Handle method to exchange data
    with TCP client.

    """

    def handle(self):
        # self.request - TCP socket connected to the client
        self.data = self.request.recv(8192).strip()
        print("{} sent:".format(self.client_address[0]))
        print(self.data)
        # just send back ACK for data arrival confirmation
        self.request.sendall("ACK from TCP Server".encode())

if __name__ == "__main__":
    HOST, PORT = "192.168.178.10", 9999

    # Init the TCP server object, bind it to the localhost on 9999 port
    tcp_server = socketserver.TCPServer((HOST, PORT), Handler_TCPServer)
    print("Maybe")
    # Activate the TCP server.
    # To abort the TCP server, press Ctrl-C.
    tcp_server.serve_forever()
