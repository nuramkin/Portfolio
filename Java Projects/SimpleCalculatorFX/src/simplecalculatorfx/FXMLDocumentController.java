/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package simplecalculatorfx;

import java.net.URL;
import java.util.ResourceBundle;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.Button;
import javafx.scene.control.TextField;

/**
 *
 * @author Nick
 */
public class FXMLDocumentController implements Initializable {
    
    Float data = 0f;
    
    char operation = ' ';
    
    @FXML
    private Button ButtonPlus;

    @FXML
    private Button Button1;

    @FXML
    private Button Button0;

    @FXML
    private Button ButtonClear;

    @FXML
    private Button ButtonEquals;
    
    @FXML
    private TextField Display;

    @FXML
    private Button ButtonMultiply;

    @FXML
    private Button ButtonMinus;

    @FXML
    private Button Button5;

    @FXML
    private Button Button4;

    @FXML
    private Button Button3;

    @FXML
    private Button Button2;

    @FXML
    private Button Button9;

    @FXML
    private Button Button8;

    @FXML
    private Button Button7;

    @FXML
    private Button Button6;

    @FXML
    private Button ButtonDivide;

    @FXML
    void handleButtonAction(ActionEvent event) {
        if(Display.getText().equals("Error")){
            Display.setText("");
        }
        
        //todo: fix error when operation buttons are pressed first
        //todo: implement decimals
        
        
        if(event.getSource() == Button0 && data != 0f){
            Display.setText("");
            Display.setText(Display.getText() + "0");
        }
        else if(event.getSource() == Button1 && data != 0f){
            Display.setText("");
            Display.setText(Display.getText() + "1");
        }
        else if(event.getSource() == Button2 && data != 0f){
            Display.setText("");
            Display.setText(Display.getText() + "2");
        }
        else if(event.getSource() == Button3 && data != 0f){
            Display.setText("");
            Display.setText(Display.getText() + "3");
        }
        else if(event.getSource() == Button4 && data != 0f){
            Display.setText("");
            Display.setText(Display.getText() + "4");
        }
        else if(event.getSource() == Button5 && data != 0f){
            Display.setText("");
            Display.setText(Display.getText() + "5");
        }
        else if(event.getSource() == Button6 && data != 0f){
            Display.setText("");
            Display.setText(Display.getText() + "6");
        }
        else if(event.getSource() == Button7 && data != 0f){
            Display.setText("");
            Display.setText(Display.getText() + "7");
        }
        else if(event.getSource() == Button8 && data != 0f){
            Display.setText("");
            Display.setText(Display.getText() + "8");
        }
        else if(event.getSource() == Button9 && data != 0f){
            Display.setText("");
            Display.setText(Display.getText() + "9");
        }
        else if(event.getSource() == Button0){
            Display.setText(Display.getText() + "0");
        }
        else if(event.getSource() == Button1){
            Display.setText(Display.getText() + "1");
        }
        else if(event.getSource() == Button2){
            Display.setText(Display.getText() + "2");
        }
        else if(event.getSource() == Button3){
            Display.setText(Display.getText() + "3");
        }
        else if(event.getSource() == Button4){
            Display.setText(Display.getText() + "4");
        }
        else if(event.getSource() == Button5){
            Display.setText(Display.getText() + "5");
        }
        else if(event.getSource() == Button6){
            Display.setText(Display.getText() + "6");
        }
        else if(event.getSource() == Button7){
            Display.setText(Display.getText() + "7");
        }
        else if(event.getSource() == Button8){
            Display.setText(Display.getText() + "8");
        }
        else if(event.getSource() == Button9){
            Display.setText(Display.getText() + "9");
        }
        else if(event.getSource() == ButtonClear){
            data = 0f;
            Display.setText("");
        }
        else if(event.getSource() == ButtonPlus){
            data = Float.parseFloat(Display.getText());
            operation = '+';//addition
            Display.setText("");
        }
        else if(event.getSource() == ButtonMinus){
            data = Float.parseFloat(Display.getText());
            operation = '-';//subtraction
            Display.setText("");
        }
        else if(event.getSource() == ButtonMultiply){
            data = Float.parseFloat(Display.getText());
            operation = '*';//Multiplication
            Display.setText("");
        }
        else if(event.getSource() == ButtonDivide){
            data = Float.parseFloat(Display.getText());
            operation = '/';//Division
            Display.setText("");
        }
        else if(event.getSource() == ButtonEquals){
            Float secondOperand = Float.parseFloat(Display.getText());
            switch(operation){
                case '+': //addition
                    Float ans = data + secondOperand;
                    Display.setText(String.valueOf(ans));
                    break;
                case '-': //subtraction
                    ans = data - secondOperand;
                    Display.setText(String.valueOf(ans));
                    break;
                case '*': //multiplication
                    ans = data * secondOperand;
                    Display.setText(String.valueOf(ans));
                    break;
                case '/': //division
                    ans = 0f;
                    try{
                        ans = data / secondOperand;
                    }catch(Exception e){
                        data = 0f;
                        Display.setText("Error");
                        break;
                    }
                    if(Float.isInfinite(ans) || Float.isNaN(ans)){
                        data = 0f;
                        Display.setText("Error");
                        break;
                    }
                    Display.setText(String.valueOf(ans));
                    break;
            }
        }
    }
    
    @Override
    public void initialize(URL url, ResourceBundle rb) {
        // TODO
    }    
    
}
