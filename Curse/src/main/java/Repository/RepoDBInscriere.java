package Repository;

import Model.Cursa;
import Model.Inscriere;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.util.ArrayList;
import java.util.List;
import java.util.Properties;

public class RepoDBInscriere implements RepoInscriere {
    private JdbcUtils dbUtils;
    private static final Logger logger = LogManager.getLogger();
    public RepoDBInscriere(Properties props) {
        dbUtils=new JdbcUtils(props);
        logger.info("Initializing RepoDBInscriere");
    }



    @Override
    public Inscriere findOne(Long id) {
        logger.traceEntry("find element by id: {}", id);
        Connection con = dbUtils.getConnection();
        Inscriere u = null;
        try(PreparedStatement preStmt = con.prepareStatement("Select * from Inscrieri where id = ?")){
            preStmt.setInt(1, id.intValue());
            try(ResultSet result = preStmt.executeQuery()){
                while(result.next()){
                    int idp = result.getInt("IDPilot");
                    int idc = result.getInt("IDCursa");
                    u = new Inscriere(idp,idc);
                    u.setId(id);
                }
            }
        }catch (Exception ex){
            logger.error(ex);
            System.err.println("Error db : " + ex);
        }
        logger.traceExit();
        return u;
    }

    @Override
    public Iterable<Inscriere> getAll() {
        logger.traceEntry("find element by id: {}");
        Connection con = dbUtils.getConnection();
        List<Inscriere> u = new ArrayList<>();
        try(PreparedStatement preStmt = con.prepareStatement("Select * from Inscrieri")){
            try(ResultSet result = preStmt.executeQuery()){
                while(result.next()){
                    int id = result.getInt("id");
                    int idp = result.getInt("IDPilot");
                    int idc = result.getInt("IDCursa");
                    Inscriere user=new Inscriere(idp,idc);
                    u.add(user);
                }
            }
        }catch (Exception ex){
            logger.error(ex);
            System.err.println("Error db : " + ex);
        }
        logger.traceExit();
        return u;
    }

    @Override
    public void add(Inscriere elem) {
        logger.traceEntry("saving user {}", elem);
        Connection con = dbUtils.getConnection();
        try(PreparedStatement preStmt = con.prepareStatement("Insert into Inscrieri(IDPilot,IDCursa) values (?, ?)")){
            preStmt.setString(1, String.valueOf(elem.getPilot()));
            preStmt.setString(2, String.valueOf(elem.getCursa()));
            int result = preStmt.executeUpdate();
            logger.trace("saved {} instances", result);
        }catch (Exception ex){
            logger.error(ex);
            System.err.println("Error db : " + ex);
        }
        logger.traceExit();
    }

    @Override
    public void delete(Inscriere elem) {
        logger.traceEntry("deleting user {}", elem);
        Connection con = dbUtils.getConnection();
        try(PreparedStatement preStmt = con.prepareStatement("Delete from Inscriere WHERE id = ?")){
            preStmt.setInt(1, elem.getId().intValue());
            int result = preStmt.executeUpdate();
            logger.trace("deleted {} instances", result);
        }catch (Exception ex){
            logger.error(ex);
            System.err.println("Error db : " + ex);
        }
        logger.traceExit();
    }

}
