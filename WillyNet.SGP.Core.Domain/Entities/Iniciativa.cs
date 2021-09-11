﻿using System.Collections.Generic;
using WillyNet.SGP.Core.Domain.Common;

namespace WillyNet.SGP.Core.Domain.Entities
{   
    public class Iniciativa : AuditableBaseEntity
    {
        private string _iniCodi;
        public int IniId { get; set; }
        public string IniNomb { get; set; }
        public string IniDescrip { get; set; }
        public int CompId { get; set; }
        public int AreaId { get; set; }
        public string UserCreaId { get; set; }
        public string UserSolicId { get; set; }
        public Componente Componente { get; set; }
        public Area Area { get; set; }
        public UserApp UserAppCrea { get; set; }
        public UserApp UserAppSolic { get; set; }
        public ICollection<Archivo> Archivos { get; set; }
        public ICollection<Flujo> Flujos { get; set; }
        public string IniCodi {
            get
            {
                this._iniCodi = "INI00" + this.IniId.ToString();
                return this._iniCodi;
            }
            set
            {
                this._iniCodi = value;
            }
        }
    }
}