using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Generated
{
    [Keyless]
    public partial class MSPC_Register
    {
        [Column(TypeName = "nvarchar(150)")]
        public string? Name { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string? Hostname { get; set; }
        [Column(TypeName = "int")]
        public long? Port { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string? Tag { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? Authority { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string? PCName { get; set; }
        public string? stringRegisteredDate { get; set; }
        public string? EnvData { get; set; }
    }
}
