package Repository;

import Domain.Cursa;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.util.ArrayList;
import java.util.List;
import java.util.Properties;

public class RepoDBCursa implements RepoCursa {
    private JdbcUtils dbUtils;
    private static final Logger logger = LogManager.getLogger();

    public RepoDBCursa(Properties props) {
        logger.info("Initializing RepoDBCursa");
        dbUtils=new JdbcUtils(props);
    }

    @Override
    public Cursa findOne(Long id) {
        logger.traceEntry("find element by id: {}", id);
        Connection con = dbUtils.getConnection();
        Cursa u = null;
        try(PreparedStatement preStmt = con.prepareStatement("Select * from Cursa where id = ?")){
            preStmt.setInt(1, id.intValue());
            try(ResultSet result = preStmt.executeQuery()){
                while(result.next()){
                    int capacitate = result.getInt("Capacitate");
                    u = new Cursa(capacitate);
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
    public Cursa findByCapacitate(int capacitate)
    {
        logger.traceEntry("find element by id: {}", capacitate);
        Connection con = dbUtils.getConnection();
        Cursa u = null;
        try(PreparedStatement preStmt = con.prepareStatement("Select * from Cursa where Capacitate = ?")){
            preStmt.setInt(1, capacitate);
            try(ResultSet result = preStmt.executeQuery()){
                while(result.next()){
                    int id = result.getInt("ID");
                    u = new Cursa(capacitate);
                    u.setId((long) id);
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
    public Iterable<Cursa> getAll() {
        logger.traceEntry("find element by id: {}");
        Connection con = dbUtils.getConnection();
        List<Cursa> u = new ArrayList<>();
        try(PreparedStatement preStmt = con.prepareStatement("Select * from Cursa")){
            try(ResultSet result = preStmt.executeQuery()){
                while(result.next()){
                    int id = result.getInt("id");
                    int capacitate=result.getInt("Capacitate");
                    Cursa user=new Cursa(capacitate);
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
    public void add(Cursa elem) {
        logger.traceEntry("saving user {}", elem);
        Connection con = dbUtils.getConnection();
        try(PreparedStatement preStmt = con.prepareStatement("Insert into Cursa(Capacitate) values (?, ?)")){
            preStmt.setString(1, String.valueOf(elem.getCapacitate()));
            int result = preStmt.executeUpdate();
            logger.trace("saved {} instances", result);
        }catch (Exception ex){
            logger.error(ex);
            System.err.println("Error db : " + ex);
        }
        logger.traceExit();
    }

    @Override
    public void delete(Cursa elem) {
        logger.traceEntry("deleting user {}", elem);
        Connection con = dbUtils.getConnection();
        try(PreparedStatement preStmt = con.prepareStatement("Delete from Cursa WHERE id = ?")){
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
