using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static mf13.modules.core.db_types;

namespace mf13.modules.core
{
    public class db_types
    {
        // --- Types ---
        public class UserTableType
        {
            public string DbName { get; set; }
            public string DbUUID { get; set; }
            public string DbType = $"#UserTable";
            public List<string> UUIDS_OFUSERS { get; set; }
            public Dictionary<string,Utils_UserTable_TblStructure> Items { get; set; }
        }
        public class MsgTableType
        {
            public string DbName { get; set; }
            public string DbUUID { get; set; }
            public string DbType = $"#MsgTable";

            public List<string> UUIDS_OFTABLES { get; set; }
            public Dictionary<string, Utils_MsgTable_TblStructure> Items { get; set; }
        }

        // --- Utils ---
        public class Utils_UserTable_TblStructure
        {
            public string UserName { get; set; }
            public string UserUUID { get; set; }

            public long MsgCount { get; set; }

            public long Likes { get; set; }
            public long Dislikes { get; set; }

            public long Pooped { get; set; }

            public List<string> Sended_To_s { get; set; }
            public List<string> Recieved_From_s { get; set; }
        }
        public class Utils_MsgTable_TblStructure
        {
            public string UserFrom { get; set; }
            public List<string> UserTo_s { get; set; }
            public List<string> Message_s { get; set; } 

        }
    }
    public class db_table
    {
        public static Dictionary<string, db_types.UserTableType> UserTables__LIST;
        public static Dictionary<string, db_types.MsgTableType> MsgTables__LIST;
    }
    public class db_manager
    {   
        // --- init ---
        public static string Init_And_Return_A_UUID(string DbName, string dbType)
        {
            string UUID = Utils_Gen_Random_UUID();

            int DbType__INT = Parse_Db_Type__INT(dbType);

            if (DbType__INT == 0)
            {
                // --- USERTABLE ---
                db_table.UserTables__LIST.Add(UUID, new db_types.UserTableType
                {
                    DbName = DbName,
                    DbUUID = UUID,
                    Items = []
                });
            }
            else if (DbType__INT == 1)
            {
                // --- USERTABLE ---
                db_table.MsgTables__LIST.Add(UUID, new db_types.MsgTableType
                {
                    DbName = DbName,
                    DbUUID = UUID,
                    Items = []
                });
            }
            else
            {
                logger.LogErr($"DbMaster-Admin: LogicError: {DbType__INT} does not relate to any types, types are: `user_table` and `messages_table`");
            }

            return UUID;
        }
        // ---

        // --- db user-table ---
        public static long Count_Items_Of_Users_Table(string UUID)
        {
            long NumItems;

            NumItems = db_table.UserTables__LIST[UUID].Items.Count;

            return NumItems;
        }
        public static List<Utils_UserTable_TblStructure> Return_Items_Of_Users_Table__LUTS(string UUID)
        {
            List<Utils_UserTable_TblStructure> Itms;

            Itms = db_table.UserTables__LIST[UUID].Items;

            return Itms;
        }
        public static Utils_UserTable_TblStructure Return_Find_A_User__UTS(string TableUUID, string UserUUID)
        {
            Utils_UserTable_TblStructure UserIdentity;

            UserIdentity = db_table.UserTables__LIST[TableUUID].Items[UserUUID];

            return UserIdentity;
        }
        public static long UTIL__Return_Count_Users__LONG(string TableUUID, string UserUUID, string SearchItem)
        {
            Utils_UserTable_TblStructure UserIdentity;

            UserIdentity = db_table.UserTables__LIST[TableUUID].Items[UserUUID];

            if (SearchItem == "pooped?")
                return UserIdentity.Pooped;
            else if (SearchItem == "likes?")
                return UserIdentity.Likes;
            else if (SearchItem == "dislikes?")
                return UserIdentity.Dislikes;
            else if (SearchItem == "msgc?")
                return UserIdentity.MsgCount;
            else 
                return -1;
        }
        public static long Return_Count_pooped_Users__LONG(string TableUUID, string UserUUID)
        {
            return UTIL__Return_Count_Users__LONG(TableUUID, UserUUID, "pooped?");
        }
        public static long Return_Count_likes_Users__LONG(string TableUUID, string UserUUID)
        {
            return UTIL__Return_Count_Users__LONG(TableUUID, UserUUID, "likes?");
        }
        public static long Return_Count_dislikes_Users__LONG(string TableUUID, string UserUUID)
        {
            return UTIL__Return_Count_Users__LONG(TableUUID, UserUUID, "dislikes?");
        }
        public static long Return_Count_Msgs_Users__LONG(string TableUUID, string UserUUID)
        {
            return UTIL__Return_Count_Users__LONG(TableUUID, UserUUID, "msgc?");
        }
        public static string Return_Username__STRING(string TableUUID, string UserUUID)
        {
            Utils_UserTable_TblStructure UserIdentity;

            UserIdentity = db_table.UserTables__LIST[TableUUID].Items[UserUUID];

            return UserIdentity.UserName;
        }
        // ---

        // --- db msg-table ---
        public static long Count_Items_Of_Msg_Table(string UUID)
        {
            long NumItems;

            NumItems = db_table.MsgTables__LIST[UUID].Items.Count;

            return NumItems;
        }
        public static List<Utils_MsgTable_TblStructure> Return_Items_Of_Msg_Table__MT(string UUID)
        {
            List<Utils_MsgTable_TblStructure> Itms;

            Itms = db_table.MsgTables__LIST[UUID].Items;

            return Itms;
        }
        public static List<Utils_UserTable_TblStructure> Return_User_With_Most_Messages__LUTS(string TableUUID, int HowManyFromTop)
        {
            long MostMsgs = 0;
            List<Utils_UserTable_TblStructure> MostMsgs_User_s = [];
            foreach (var User in db_table.UserTables__LIST[TableUUID].UUIDS_OFUSERS)
            {
                Utils_UserTable_TblStructure UserIdentity = db_table.UserTables__LIST[TableUUID].Items[User];
                long x = UserIdentity.MsgCount;

                if (x > MostMsgs)
                {
                    MostMsgs = x;
                    MostMsgs_User_s = [];
                    MostMsgs_User_s.Add(UserIdentity);
                }
                else if (x == MostMsgs)
                {
                    MostMsgs_User_s.Add(UserIdentity);
                }
            }
            return MostMsgs_User_s;
        }
        // ---


        // --- utils function ---
        public static string Utils_Gen_Random_UUID()
        {
            var x = int.Parse(DateTime.Now.ToString("ss")) * 24;
            var y = int.Parse(DateTime.Now.ToString("ss")) * 48 - (-4 / x);
            var z = int.Parse(DateTime.Now.ToString("ss")) * 96 - (-5 / y);

            return $"{x}-{y}-{z}";
        }
        public static int Parse_Db_Type__INT(string DbRawType)
        {
            int x = 70;

            if (DbRawType.ToLower().StartsWith("user_table"))
                x = 0;
            else if (DbRawType.ToLower().StartsWith("messages_table"))
                x = 1;
            else
                x = 70;

            if (x >= 70)
                return x;
            else if (x == 0)
                return x;
            else if (x == 1)
                return x;
            else
            {
                logger.LogErr($"Unknoun: {x}: LogicError: {x} is less then 70 but still not recognized as a type!");
                return x;
            }
        }
        // ---
    }
}
