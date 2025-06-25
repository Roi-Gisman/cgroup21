using System.Collections.Generic;

namespace WebApplication1.BL
{
    public class User
    {
        int id;
        string name;
        string email;
        string password;
        bool active;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public bool Active { get => active; set => active = value; }

        
        public User(int id,string name, string email, string password, bool active)
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.password = password;
            this.active = active;
        }

        public User() { }

        public int Insert()
        {
            DBservices dbs = new DBservices();
            return dbs.InsertUser(this);
        }


        public User GetUserByEmailAndPassword(string email,string password)
        {
            DBservices dbs = new DBservices();
            return dbs.GetUserByEmailPassword(email,password);
        }
        public int Delete(int id)
        {
            DBservices dbs = new DBservices();
            return dbs.DeleteUser(id);
        }
        public int UpdateUser(int id, User user)
        {
            DBservices dbs = new DBservices();
            return dbs.UpdateUser(id,user);
        }  
        public List<User> GetAllUsers()
        {
            DBservices dbs = new DBservices();
            return dbs.GetUsers();
        }
        public User GetUserById(int id)
        {
            DBservices dbs = new DBservices();
            return dbs.GetUserId(id);
        }
    }
}