using Microsoft.Data.SqlClient;
using System.Data;
using WebAPI_Practice.Model;

namespace WebAPI_Practice.Data
{
    public class StateRepository
    {
        #region Configuration
        private readonly string _connectionString;
        public StateRepository(IConfiguration configuration)
        {

            _connectionString = configuration.GetConnectionString("ConnectionString");
        }
        #endregion

        #region SelectAllStates
        public IEnumerable<StateModel> SelectAllStates()
        {
            var states = new List<StateModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_LOC_State_SelectAll", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    states.Add(new StateModel()
                    {
                        StateID = Convert.ToInt32(reader["StateID"]),
                        StateName = reader["StateName"].ToString(),
                        StateCode = reader["StateCode"].ToString(),
                        CountryName = reader["CountryName"].ToString(),
                        CountryID = Convert.ToInt32(reader["CountryID"]),
                    });
                }
            }
            return states;
        }
        #endregion

        #region InsertState
        public bool InsertState(StateModel state)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_LOC_State_Insert", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@StateName", state.StateName);
                cmd.Parameters.AddWithValue("@StateCode", state.StateCode);
                cmd.Parameters.AddWithValue("@CountryID", state.CountryID);
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);


                connection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;

            }
        }
        #endregion

        #region UpdateState
        public bool UpdateState(StateModel state)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_LOC_State_Update", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@StateID", state.StateID);
                cmd.Parameters.AddWithValue("@StateName", state.StateName);
                cmd.Parameters.AddWithValue("@StateCode", state.StateCode);
                cmd.Parameters.AddWithValue("@CountryID", state.CountryID);
                cmd.Parameters.AddWithValue("@ModifiedDate", SqlDbType.DateTime).Value = DBNull.Value;

                connection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;

            }
        }
        #endregion

        #region DeleteState
        public bool DeleteState(int stateID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_LOC_State_Delete", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@StateID", stateID);

                connection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0; // Returns true if at least one row is affected, indicating a successful delete
            }
        }
        #endregion

        #region GetStateByID
        public IEnumerable<StateModel> GetStateByID(int stateID)
        {
            var stateByID = new List<StateModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_LOC_State_SelectByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@StateID", stateID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    stateByID.Add(new StateModel
                    {
                        StateID = Convert.ToInt32(reader["StateID"]),
                        StateName = reader["StateName"].ToString(),
                        StateCode = reader["StateCode"].ToString(),
                        CountryID = Convert.ToInt32(reader["CountryID"]),
                    });
                }
            }
            return stateByID;
        }
        #endregion
    }
}
