using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
 
namespace RDM_API.Models
{
    /// <summary>
    /// Class of functions used for handling requests.
    /// </summary>
    public class RDM_Functions : IRDM_Functions
    {
        private List<SessionEvent> sessions;
        private IConfiguration configuration;

        /// <summary>
        /// Constructer run to load data from file.
        /// </summary>
        public RDM_Functions(IConfiguration configuration)
        {
            this.configuration = configuration;
            loadSessions();
        }

        /// <summary>
        /// Saves state of list to file for long term storage.
        /// </summary>
        private void saveSessions()
        {
            string json = System.Text.Json.JsonSerializer.Serialize(sessions);
            string path = configuration["FilePath"];
            File.WriteAllText(path, json);
        }

        /// <summary>
        /// Gets list of all in use sessions
        /// </summary>
        /// <returns>List of in use sessions</returns>
        public List<SessionEvent> getSessions()
        {
            return sessions;
        }

        /// <summary>
        /// Loads data from Sessions.json.
        /// </summary>
        private void loadSessions()
        {
            string path = configuration["FilePath"];
            string json = File.ReadAllText(path);
            sessions = System.Text.Json.JsonSerializer.Deserialize<List<SessionEvent>>(json);
            if (sessions == null)
            {
                sessions = new List<SessionEvent>();
            }
        }

        /// <summary>
        /// Gets number of users currently connected to RDM
        /// </summary>
        /// <returns>Number of sessions occupied</returns>
        public int countSessions()
        {
            return sessions.Count();
        }

        /// <summary>
        /// Checks list for pre existing session with identical ID
        /// </summary>
        /// <param name="SessionId">$SESSION_ID$</param>
        /// <returns>bool, true if session in use, false if free.</returns>
        public bool sessionExists(string SessionId)
        {
            bool sesionExists = sessions.Exists(x => x.DateTime.Date == DateTime.Now.Date && x.sessionId == SessionId);
            return sessions.Exists(x => x.DateTime.Date == DateTime.Now.Date && x.sessionId == SessionId);
        }

        /// <summary>
        /// Checks for and handles null Session ID's, checks for session availability and adds sesssion.
        /// </summary>
        /// <param name="SessionId">$SESSION_ID$</param>
        /// <param name="userId">$USERNAME$</param>
        /// <returns>bool designating succes of operation</returns>
        public bool AddSessionAndUserId(string SessionId, string userId, string sessionName)
        {
            bool canAddSession;
            if (SessionId != null)
            {
                if (!sessionExists(SessionId))
                {
                    SessionEvent session = new SessionEvent(SessionId, userId);
                    session.DateTime = DateTime.Now;
                    session.sessionId = SessionId;
                    session.sessionName = sessionName;
                    session.userId = userId;
                    session.open = false;
                    sessions.Add(session);
                    canAddSession = true;
                    saveSessions();
                }
                else
                {
                    canAddSession = false;
                }
            }
            else
            {
                canAddSession = false;
            }
            return canAddSession;
        }

        /// <summary>
        /// Removes sessions older than current time with common Id
        /// </summary>
        /// <param name="SessionId">$SESSION_ID$</param>
        public void removeOldSession(string SessionId)
        {
            int test = sessions.RemoveAll(x => x.sessionId == SessionId && x.DateTime < DateTime.Now);
        }


        /// <summary>
        /// finds user on session when given $SESSION_ID$
        /// </summary>
        /// <param name="sessionId">$SESSION_ID$</param>
        /// <returns>name of user on session</returns>
        public string getSessionUser(string sessionId)
        {
            if (sessionExists(sessionId))
            {
                return (from session in sessions
                        where session.sessionId == sessionId && session.DateTime.Date == DateTime.Now.Date
                        select session).First<SessionEvent>().userId;
            }
            else
            {
                return "";
            }
        }


        /// <summary>
        /// Removes all sessions
        /// </summary>
        public void clearAllSessions()
        {
            sessions.Clear();
            saveSessions();
        }

        /// <summary>
        /// deletes session with matching $SESSION_ID$ and $USERNAME$
        /// </summary>
        /// <param name="SessionId">$SESSION_ID$</param>
        /// <param name="userId">$USERNAME$</param>
        /// <returns>bool designating operation success</returns>
        public bool deleteSession(string SessionId, string userId)
        {
            if (sessionExists(SessionId))
            {
                sessions.RemoveAll(x => x.sessionId == SessionId && x.userId == userId);
                saveSessions();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// deletes all sessions associated with user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>bool designating operation success</returns>
        public bool deleteUserSessions(string userId)
        {
            sessions.RemoveAll(x => x.userId == userId);
            saveSessions();
            return true;
        }

        /// <summary>
        /// Deletes sessions by session Id only
        /// </summary>
        /// <param name="sessionId">ID number of RDM Session</param>
        /// <returns>Succes state of operation</returns>
        public bool DeleteBySessions(string sessionId)
        {
            sessions.RemoveAll(x => x.sessionId == sessionId);
            return true;
        }

        public void changeTakeoverToAllow()
        {

        }
    }
}

 