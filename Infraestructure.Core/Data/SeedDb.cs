using Infraestructure.Entity.Models.General;
using Infraestructure.Entity.Models.Logistic;
using Infraestructure.Entity.Models.Security;
using Logistic.Common.Enums;
using NETCore.Encrypt;

namespace Infraestructure.Core.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;


        #region Builder
        public SeedDb(DataContext context)
        {
            _context = context;
        }
        #endregion

        public async Task ExecSeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            await CheckPermissionAsync();
            await CheckRolAsync();
            await CheckRolPermissonAsync();
            await CheckUserAsync();
            await CheckCountryAsync();
            await CheckSeaportAsync();
            await CheckWarehousesAsync();
            await CheckClientAsync();
        }

        private async Task CheckPermissionAsync()
        {
            if (!_context.PermissionEntity.Any())
            {
                _context.PermissionEntity.AddRange(new List<PermissionEntity>
                {
                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.CrearUsuarios,
                        Ambit="Usuarios",
                        Permission="Crear Usuarios",
                        Description="Crear usuarios en el sistema"
                    },
                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.ActualizarUsuarios,
                        Ambit="Usuarios",
                        Permission="Actualizar Usuarios",
                        Description="Actualizar datos de un usuario en el sistema"
                    },
                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.EliminarUsuarios,
                        Ambit="Usuarios",
                        Permission="Eliminar Usuarios",
                        Description="Eliminar un usuairo del sistema"
                    },
                    new PermissionEntity
                    {
                        IdPermission =(int) Enums.Permission.ConsultarUsuarios,
                        Ambit = "Usuarios",
                        Permission="Consultar Usuarios",
                        Description="Consulta todos los usuarios"
                    },
                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.ActualizarRoles,
                        Ambit="Roles",
                        Permission="Actualizar Roles",
                        Description="Actualizar datos de un Roles en el sistema"
                    },
                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.ConsultarRoles,
                        Ambit="Roles",
                        Permission="Consultar Roles",
                        Description="Consultar Roles del sistema"
                    },
                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.ActualizarPermisos,
                        Ambit = "Permisos",
                        Permission="Actualizar Permisos",
                        Description="Actualizar datos de un Permiso en el sistema"
                    },
                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.ConsultarPermisos,
                        Ambit = "Permisos",
                        Permission="Consultar Permisos",
                        Description="Consultar Permisos del sistema"
                    },
                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.DenegarPermisos,
                        Ambit = "Permisos",
                        Permission="Denegar Permisos Rol",
                        Description="Denegar permisos a un rol del sistema"
                    },
                });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckRolAsync()
        {
            if (!_context.RolEntity.Any())
            {
                _context.RolEntity.AddRange(new List<RolEntity>
                {
                    new RolEntity
                    {
                        IdRol=(int)Enums.RolUser.Administrador,
                        Rol="Administrador"
                    },
                    new RolEntity
                    {
                        IdRol=(int)Enums.RolUser.Estandar,
                        Rol="Estandar"
                    }
                });

                await _context.SaveChangesAsync();
            }
        }

        //Asignamos todos los permisos al rol administrador
        private async Task CheckRolPermissonAsync()
        {
            if (!_context.RolesPermissionsEntity.Where(x => x.IdRol == (int)Enums.RolUser.Administrador).Any())
            {
                var rolesPermisosAdmin = _context.PermissionEntity.Select(x => new RolesPermissionsEntity
                {
                    IdRol = (int)Enums.RolUser.Administrador,
                    IdPermission = x.IdPermission
                }).ToList();

                _context.RolesPermissionsEntity.AddRange(rolesPermisosAdmin);

                await _context.SaveChangesAsync();
            }
        }

        //Creamos un usuario por defecto
        private async Task CheckUserAsync()
        {
            if (!_context.UserEntity.Where(x => x.IdRol == (int)Enums.RolUser.Administrador).Any())
            {
                UserEntity user = new UserEntity()
                {
                    IdRol = (int)Enums.RolUser.Administrador,
                    Name = "Maria Clara",
                    LastName = "Jimenez",
                    Email = "mariac.jimenez@ingeneo.com",
                    Password = EncryptProvider.Sha256("123456"),
                };

                _context.UserEntity.Add(user);

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCountryAsync()
        {
            if (!_context.CountryEntity.Any())
            {
                _context.CountryEntity.AddRange(new List<CountryEntity>
                {
                    new CountryEntity
                    {
                       Country="Colombia"
                    },
                });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckSeaportAsync()
        {
            if (!_context.SeaportEntity.Any())
            {
                CountryEntity country = _context.CountryEntity.FirstOrDefault();
                _context.SeaportEntity.AddRange(new List<SeaportEntity>
                {
                    new SeaportEntity
                    {
                       Seaport="Puerto Candelaria",
                       IdCountry=country.Id
                    },
                    new SeaportEntity
                    {
                       Seaport="Puerto Nuevo Antioquia",
                       IdCountry=country.Id
                    },
                });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckWarehousesAsync()
        {
            if (!_context.WarehouseEntity.Any())
            {
                CountryEntity country = _context.CountryEntity.FirstOrDefault();
                _context.WarehouseEntity.AddRange(new List<WarehouseEntity>
                {
                    new WarehouseEntity
                    {
                       Warehouse="Bodega SutiMax",
                       Direction="Principal 20",
                       IdCountry=country.Id
                    },
                    new WarehouseEntity
                    {
                       Warehouse="Bodega La Esmeralda",
                       Direction="Calle 70 Edificio #74-54",
                       IdCountry=country.Id
                    },
                });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckClientAsync()
        {
            if (!_context.ClientEntity.Any())
            {
                CountryEntity country = _context.CountryEntity.FirstOrDefault();
                _context.ClientEntity.AddRange(new List<ClientEntity>
                {
                    new ClientEntity
                    {
                       Name="Pedro",
                       LastName="Infante",
                       Direction="Avenida 80",
                       Email="pedro@infante.com",
                       Phone="3216549877",
                       IdCountry=country.Id
                    },
                    new ClientEntity
                    {
                       Name="Karol",
                       LastName="G",
                       Direction="Medellin",
                       Email="karolg@karol.com",
                       Phone="3204568596",
                       IdCountry=country.Id
                    },
                });

                await _context.SaveChangesAsync();
            }
        }


    }

}
