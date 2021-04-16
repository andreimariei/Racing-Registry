package Controller;

import Model.Angajat;
import Model.Cursa;
import Model.Pilot;
import Service.Service;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.fxml.FXML;
import javafx.scene.Node;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.TextField;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.input.MouseEvent;



import static java.lang.Integer.parseInt;

public class MainPageControl {

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

    private Service srv;
    private Angajat angajat;

    public void setSrv(Angajat angajat, Service srv) {
        this.srv=srv;
        this.angajat=angajat;
        init();
    }

    private void init() {
        CurseTable.setItems(modelCursa);
        PilotTable.setItems(modelPilot);

    }

    ObservableList<Pilot> modelPilot= FXCollections.observableArrayList();
    ObservableList<Cursa> modelCursa=FXCollections.observableArrayList();

    public void ArataParticipanti(MouseEvent mouseEvent) {

        String numeEchipa=NumeEchipaText.getText();
        int capacitate=parseInt(CapacitateText.getText());
        modelPilot.setAll(srv.getInscrisiEchipa(numeEchipa,capacitate));
        PilotTable.setItems(modelPilot);

        IDPilot.setCellValueFactory(new PropertyValueFactory<Pilot,Long>("Id"));
        NumePilot.setCellValueFactory(new PropertyValueFactory<Pilot,String>("Nume"));
        NumeEchipa.setCellValueFactory(new PropertyValueFactory<Pilot,String>("NumeEchipa"));
    }

    public void InscrieParticipant(MouseEvent mouseEvent) {
        String echipa=NumeEchipaText.getText();
        String pilot=NumePilotText.getText();
        int id=parseInt(IdPilot.getText());
        int capacitate=parseInt(CapacitateText.getText());
        srv.addInscriere(id,pilot,echipa,capacitate);
    }

    public void ArataCurse(MouseEvent mouseEvent) {
        modelCursa.setAll(srv.getCurse());
        CurseTable.setItems(modelCursa);
        IDCursa.setCellValueFactory(new PropertyValueFactory<Cursa,Long>("Id"));
        Capacitate.setCellValueFactory(new PropertyValueFactory<Cursa,Integer>("Capacitate"));
    }

    public void Logout(MouseEvent mouseEvent) {
        ((Node)(mouseEvent.getSource())).getScene().getWindow().hide();
    }
}
