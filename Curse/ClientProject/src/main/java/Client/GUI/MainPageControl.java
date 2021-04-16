package Client.GUI;

import Domain.Angajat;
import Domain.Cursa;
import Domain.Inscriere;
import Domain.Pilot;
import Services.ICursaObserver;
import Services.ICursaService;
import Services.ProjectException;
import javafx.application.Platform;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.TextField;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.input.MouseEvent;

import java.net.URL;
import java.sql.SQLOutput;
import java.util.List;
import java.util.ResourceBundle;

import static java.lang.Integer.parseInt;

public class MainPageControl implements Initializable,ICursaObserver {

    @FXML
    TableView PilotTable;
    @FXML
    TableView CurseTable;
    @FXML
    TableColumn IDPilot;
    @FXML
    TableColumn IDCursa;
    @FXML
    TableColumn NumePilot;
    @FXML
    TableColumn NumeEchipa;
    @FXML
    TableColumn Capacitate;
    @FXML
    TextField NumePilotText;
    @FXML
    TextField NumeEchipaText;
    @FXML
    TextField CapacitateText;
    @FXML
    TextField IdPilot;

    private ICursaService srv;
    private Angajat angajat;

    public void setSrv(ICursaService srv) {
        this.srv=srv;
        try {
            init();
        } catch (ProjectException e) {
            e.printStackTrace();
        }
    }
    public void setAngajat(Angajat angajat)
    {
        this.angajat=angajat;
    }

    public void init() throws ProjectException {

        CurseTable.setItems(modelCursa);
        IDCursa.setCellValueFactory(new PropertyValueFactory<Cursa,Long>("Id"));
        Capacitate.setCellValueFactory(new PropertyValueFactory<Cursa,Integer>("Capacitate"));


        PilotTable.setItems(modelPilot);
        IDPilot.setCellValueFactory(new PropertyValueFactory<Pilot,Long>("Id"));
        NumePilot.setCellValueFactory(new PropertyValueFactory<Pilot,String>("Nume"));
        NumeEchipa.setCellValueFactory(new PropertyValueFactory<Pilot,String>("NumeEchipa"));


        try {
            modelCursa.setAll((List<Cursa>) srv.getCurse());
        } catch (ProjectException e) {
            e.printStackTrace();
        }
        List<Pilot> p= (List<Pilot>) srv.getPiloti();
        for(Pilot x:p)
            System.out.println(x);
        try {
            modelPilot.setAll((List<Pilot>) srv.getPiloti());
        } catch (ProjectException e) {
            e.printStackTrace();
        }
    }

    ObservableList<Pilot> modelPilot= FXCollections.observableArrayList();
    ObservableList<Cursa> modelCursa=FXCollections.observableArrayList();

    public void ArataParticipanti(MouseEvent mouseEvent) {

        String numeEchipa=NumeEchipaText.getText();
        int capacitate=parseInt(CapacitateText.getText());
        try {
            modelPilot.setAll(srv.getInscrisiEchipa(numeEchipa,capacitate));
        } catch (ProjectException e) {
            e.printStackTrace();
        }
        PilotTable.setItems(modelPilot);

        IDPilot.setCellValueFactory(new PropertyValueFactory<Pilot,Long>("Id"));
        NumePilot.setCellValueFactory(new PropertyValueFactory<Pilot,String>("Nume"));
        NumeEchipa.setCellValueFactory(new PropertyValueFactory<Pilot,String>("NumeEchipa"));
        NumeEchipaText.clear();
        CapacitateText.clear();
    }

    public void InscrieParticipant(MouseEvent mouseEvent) {
        String echipa=NumeEchipaText.getText();
        String pilot=NumePilotText.getText();
        int capacitate=parseInt(CapacitateText.getText());
        String idpilot=IdPilot.getText();
        int idpilot1=0;
        if(idpilot!="")
        {
             idpilot1=parseInt(idpilot);
        }
        try {
            srv.addInscriere(idpilot1,pilot,echipa,capacitate);
        } catch (ProjectException e) {
            e.printStackTrace();
        }
        NumeEchipaText.clear();
        NumeEchipaText.clear();
        CapacitateText.clear();
        IdPilot.clear();
    }

    public void ArataCurse(MouseEvent mouseEvent) {

        try {
            modelCursa.setAll((List<Cursa>) srv.getCurse());
        } catch (ProjectException e) {
            e.printStackTrace();
        }

        CurseTable.setItems(modelCursa);
        IDCursa.setCellValueFactory(new PropertyValueFactory<Cursa,Long>("Id"));
        Capacitate.setCellValueFactory(new PropertyValueFactory<Cursa,Integer>("Capacitate"));
    }

    public void logout() {
        try{
            srv.logout(angajat,this);
        }catch (ProjectException e){
            System.out.println("Logout error"+e);}
    }



    @Override
    public void initialize(URL location, ResourceBundle resources) {

    }

    @Override
    public void inscriereAdded(Pilot pilot) throws ProjectException {
        Platform.runLater(()->
        {modelPilot.add(pilot);
            System.out.println(pilot.toString());
        PilotTable.refresh();
        CurseTable.refresh();}
        );

    }
}
