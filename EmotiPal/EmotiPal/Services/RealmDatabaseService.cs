using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotiPal.Helpers;
using EmotiPal.Models;
using Realms;
using Realms.Sync;

namespace EmotiPal.Services
{
    public class RealmDatabaseService
    {
        public static Realms.Sync.App RealmApp;
        private Realm sentimentRealm;
        private ObservableCollection<SentimentResult> _sentimentResults = new ObservableCollection<SentimentResult>();
        private string projectionPartition;       

        public RealmDatabaseService()
        {           
        }        

        public ObservableCollection<SentimentResult> SentimentResults
        {
            get
            {
                return _sentimentResults;
            }
        }

        public async Task LogSentimentResult(SentimentResult result)
        {
           try
            {
                sentimentRealm = await Realm.GetInstanceAsync(MigrateSchema());
                await sentimentRealm.Write(async () =>
                {
                    var response = sentimentRealm.Add(result);
                });
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<ObservableCollection<SentimentResult>> GetAllSentimentResults()
        {
            try
            {
                sentimentRealm = await Realm.GetInstanceAsync(MigrateSchema());
                _sentimentResults = new ObservableCollection<SentimentResult>(sentimentRealm.All<SentimentResult>().ToList());               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return _sentimentResults;
        }
        
        private RealmConfiguration MigrateSchema()
        {
            var config = new RealmConfiguration
            {
                SchemaVersion = 2,
                MigrationCallback = (migration, oldSchemaVersion) =>
                {
                    var oldSentimentResult = migration.OldRealm.DynamicApi.All("SentimentResult");
                    var newSentimentResult = migration.NewRealm.All<SentimentResult>();
                }
            };

            return config;
        }
    }
}
