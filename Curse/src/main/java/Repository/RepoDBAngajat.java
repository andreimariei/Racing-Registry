package Repository;

import Model.Angajat;
import Model.Cursa;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.util.ArrayList;
import java.util.List;
import java.util.Properties;



public class RepoDBAngajat implements RepoAngajat{
    private JdbcUtils dbUtils;
    private static final Logger logger = LogManager.getLogger();

    public RepoDBAngajat(Properties prop) {
        logger.info("Initializing RepoDBAngajat");
        dbUtils=new JdbcUtils(prop);
    }


    @Override
    public Angajat findOne(Long id) {
        logger.traceEntry("find element by id: {}", id);
        Connection con = dbUtils.getConnection();
        Angajat u = null;
        try(PreparedStatement preStmt = con.prepareStatement("Select * from Angajati where ID = ?")){
            preStmt.setInt(1, id.intValue());
            try(ResultSet result = preStmt.executeQuery()){
                while(result.next()){
                    String username = result.getString("UsernameA");
                    String password = result.getString("PasswordA");
                    u = new Angajat(username, password);
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
    public Iterable<Angajat> getAll() {
        logger.traceEntry("find element by id: {}");
        Connection con = dbUtils.getConnection();
        List<Angajat> u = new ArrayList<>();
        try(PreparedStatement preStmt = con.prepareStatement("Select * from Angajati")){
            try(ResultSet result = preStmt.executeQuery()){
                while(result.next()){
                    int id = result.getInt("id");
                    String username = result.getString("UsernameA");
                    String password = result.getString("PasswordA");
                    Angajat user = new Angajat(username, password);
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
    public void add(Angajat elem)
    {
        logger.traceEntry("saving user {}", elem);
        Connection con = dbUtils.getConnection();
        try(PreparedStatement preStmt = con.prepareStatement("Insert into Angajati (UsernameA, PasswordA) values (?, ?)")){
            preStmt.setString(1, elem.getUsername());
            preStmt.setString(2, elem.getPassword());
            int result = preStmt.executeUpdate();
            logger.trace("saved {} instances", result);
        }catch (Exception ex){
            logger.error(ex);
            System.err.println("Error db : " + ex);
        }
        logger.traceExit();
    }



    @Override
    public void delete(Angajat elem) {
        logger.traceEntry("deleting user {}", elem);
        Connection con = dbUtils.getConnection();
        try(PreparedStatement preStmt = con.prepareStatement("Delete from Angajati WHERE ID = ?")){
            preStmt.setInt(1, elem.getId().intValue());
            int result = preStmt.executeUpdate();
            logger.trace("deleted {} instances", result);
        }catch (Exception ex){
            logger.error(ex);
            System.err.println("Error db : " + ex);
        }
        logger.traceExit();
    }

    @Override
    public Angajat findByUsername(String username) {
       /*logger.traceEntry("find element by username: {}", username);
        Connection con = dbUtils.getConnection();
        Angajat u = null;
        try(PreparedStatement preStmt = con.prepareStatement("Select * from Angajati where UsernameA = ?")){
            preStmt.setString(1, username);
            try(ResultSet result = preStmt.executeQuery()){
                while(result.next()){
                    Long id=result.getLong("ID");
                    String password = result.getString("PasswordA");
                    u = new Angajat(username, password);
                    u.setId(id);
                }
            }
        }catch (Exception ex){
            logger.error(ex);
            System.err.println("Error db : " + ex);
        }
        logger.traceExit();
*/

        logger.traceEntry();
        Connection con = dbUtils.getConnection();
        Angajat employee = null;
        try(PreparedStatement preStmt = con.prepareStatement("Select * from Angajati")){
            try(ResultSet result = preStmt.executeQuery())
            {
                while(result.next())
                {
                    Long id = result.getLong("ID");
                    String username1 = result.getString("UsernameA");
                    String password = result.getString("PasswordA");
                    System.out.println(username+"**********************************");
                    if(username1.equals(username)) {
                        employee = new Angajat(username,password);
                        employee.setId(id);
                    }
                }
            }
        } catch (Exception e){
            logger.error(e);
            System.err.println("Error DB "+e);
        }
        logger.traceExit(employee);
        System.out.println(employee.username+"+++++++++++++++");
        return  employee;
    }
}
