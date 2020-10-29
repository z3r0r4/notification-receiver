public static void main(String[] args) {
    java.awt.EventQueue.invokeLater(new Runnable(){
        public void run(){
            new NotificationServer().setVisible(true);
        }
    });
    try {
        ss = new ServerSocket(6000);
        while(true){
            s = ss.accept();
            isr = new InputStreamReader();
            
        }
    } catch (Exception e) {
        //TODO: handle exception
    }
}