using MongoDB.Driver;
using Senai.Gufos.WebApi.Domains;
using Senai.Gufos.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Gufos.WebApi.Repositories {
    public class LocalizacaoRepository : ILocalizacaoRepository {

        private readonly IMongoCollection<Localizacoes> _localizacoes;

        public LocalizacaoRepository () {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("t_gufos");
            _localizacoes = database.GetCollection<Localizacoes>("mapas");
        }

        public void Cadastrar (Localizacoes loc) {
            _localizacoes.InsertOne(loc);
        }

        public List<Localizacoes> Listar () {
            return _localizacoes.Find(x => true).ToList();
        }
    }
}