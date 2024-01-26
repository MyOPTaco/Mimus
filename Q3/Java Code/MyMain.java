import javax.swing.JFrame;
import javax.awt.Color;
public class MyMain {
    
    //Timer timer;
    //Font font1 font2;
    
    
    public static void main(String args[]) {
        
        CookieMain();
      //System.out.println("The Button App");
    }
    
    
    public static void CookieMain(){
        
        //derekDollarCounter = 0;
        //timerOn = false;
        //randomizedSeconds = 0;
        
        createUI();
    }
    
    //public void createFont(){
    //    
   //     font1 = new Font("Sans Serif", font.PLAIN, 24);
   //     font2 = new Font("Sans Serif", font.PLAIN, 12);
        
   // }
    
    public static void createUI(){
        JFrame window = new JFrame();
        window.setSize(360, 800);
        window.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        window.getContentPane().setBackground(Color.black);
        window.setLayout(null);
        
        window.setVisible(true);
    }
}