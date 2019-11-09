/**
 * \brief
 *   Authorization API Info
 *
 * \section Authorization API Info
 *    <table>
 *      <tr><th>API
 *      <tr><th>API Docs
 *          <td><a href='https://https://delphixpoc/api/#authorization'>
 *              https://https://delphixpoc/api/#authorization</a>
 *    </table>

 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelphixLibrary
{
    public static class EnvironmentVariables
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        //Delphix environment variables.  Tests rely on these being set. For regular use, these can be passed in CreateSession.
        public static string DelphixUser { get; set; }
        public static string DelphixPassword { get; set; }
        public static string DelphixUrl { get; set; }

        //Some configuration settings.
        public static int maxRetries { get; set; }         // Max amount of times to retry waiting for "millisecondsWait".  I default to 15 retries for a total of 15 minutes waiting.
        public static int millisecondsWait { get; set; }   // Milliseconds to wait during each retry.  I default to 60000 (60 seconds)


        public static void SetDelphixEnvironmentVariables(int maxRetrySetting = 3)
        {
            try {
                DelphixUser = System.Environment.GetEnvironmentVariable("DELPHIX_USER");
                if (DelphixUser == null)
                {
                    logger.Info(@"DELPHIX_USER environment variable was not found or set.  Set this using 'setx DELPHIX_USER secretusername' in CMD , or pass the username in CreateSession");
                }

                DelphixPassword = System.Environment.GetEnvironmentVariable("DELPHIX_PASSWORD");
                if (DelphixPassword == null)
                {
                    logger.Info(@"DELPHIX_PASSWORD environment variable was not found or set.  Set this using 'setx DELPHIX_PASSWORD secretpassword' in CMD , or pass the password in CreateSession");
                }

                DelphixUrl = System.Environment.GetEnvironmentVariable("DELPHIX_URL");
                if (DelphixUrl == null)
                {
                    logger.Info(@"DELPHIX_URL environment variable was not found or set.  Set this using 'setx DELPHIX_URL secretpassword' in CMD , or pass the url in CreateSession");
                }

            }

            catch (Exception ex) {
                logger.Warn("Something unexpected happened during EnvironmentVariables.SetDelphixEnvironmentVariables : ");
                logger.Error(ex);
            };
        }

        public static void SetConfig(int maxRetrySetting = 15, int millisecondsToWait = 60000)
        {
            maxRetries = maxRetrySetting;
            logger.Info("Max retry setting for API calls was set to: " + maxRetries.ToString());

            millisecondsWait = millisecondsToWait;
            logger.Info("Milliseconds per retry set to : " + millisecondsWait.ToString());
        }


    }
}
