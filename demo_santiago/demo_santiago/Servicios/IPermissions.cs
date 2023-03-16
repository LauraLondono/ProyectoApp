// Mateo 6:33: Mas buscad primeramente el reino de Dios y su justicia, y todas estas cosas os serán añadidas.
using System;
using System.Threading.Tasks;

namespace demo_santiago
{
    public interface IPermissions
    {
        Task<bool> PermissionMethod(string permission);
    }
}
