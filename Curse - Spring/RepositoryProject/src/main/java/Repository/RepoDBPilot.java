package Repository;

import Domain.Pilot;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.util.ArrayList;
import java.util.List;
import java.util.Properties;

public class RepoDBPilot implements RepoPilot {
    private JdbcUtils dbUtils;
    private static final Logger logger = LogManager.getLogger();

    public RepoDBPilot(Properties props) {
        logger.info("Initializing RepoDBPilot");
        dbUtils=new JdbcUtils(props);
    }

    @Override
    public Pilot findOne(Long id) {
        logger.traceEntry("find element by id: {}", id);
        Connection con = dbUtils.getConnection();
        Pilot u = null;
        try(PreparedStatement preStmt = con.prepareStatement("Select * from Pilot where id = ?")){
            preStmt.setInt(1, id.intValue());
            try(ResultSet result = preStmt.executeQuery()){
                while(result.next()){
                    String nume = result.getString("Nume");
                    String numeEchipa = result.getString("NumeEchipa");
                    u = new Pilot(nume,numeEchipa);
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
    public Iterable<Pilot> getAll() {
        logger.traceEntry("find element by id: {}");
        Connection con = dbUtils.getConnection();
        List<Pilot> u = new ArrayList<>();
        try(PreparedStatement preStmt = con.prepareStatement("Select * from Pilot")){
            try(ResultSet result = preStmt.executeQuery()){
                while(result.next()){
                    int id = result.getInt("id");
                    String nume = result.getString("Nume");
                    String numeEchipa = result.getString("NumeEchipa");
                    Pilot user = new Pilot(nume, numeEchipa);
                    user.setId((long)id);
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
    public void add(Pilot elem) {
        logger.traceEntry("saving user {}", elem);
        Connection con = dbUtils.getConnection();
        try(PreparedStatement preStmt = con.prepareStatement("Insert into Pilot (Nume, NumeEchipa) values (?, ?)")){
            preStmt.setString(1, elem.getNume());
            preStmt.setString(2, elem.getNumeEchipa());
            int result = preStmt.executeUpdate();
            logger.trace("saved {} instances", result);
        }catch (Exception ex){
            logger.error(ex);
            System.err.println("Error db : " + ex);
        }
        logger.traceExit();
    }

    @Override
    public void delete(Pilot elem) {
        logger.traceEntry("deleting user {}", elem);
        Connection con = dbUtils.getConnection();
        try(PreparedStatement preStmt = con.prepareStatement("Delete from Pilot WHERE id = ?")){
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
