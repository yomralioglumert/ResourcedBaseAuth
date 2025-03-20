using Authentication.Database.Models;

namespace Authentication.Database;

public static class SeedData
{
    public static void Initialize(ApplicationDbContext context)
    {
        if (context.Permissions.Any())
            return;

        var permissions = new[]
        {
            new Permission
            {
                Name = "Kullanıcı Listeleme",
                Description = "Tüm kullanıcıları listeleme yetkisi",
                Endpoint = "/api/user",
                Method = "GET"
            },
            new Permission
            {
                Name = "Kullanıcı Detay Görüntüleme",
                Description = "Tek bir kullanıcının detaylarını görüntüleme yetkisi",
                Endpoint = "/api/user/{id}",
                Method = "GET"
            },
            new Permission
            {
                Name = "Kullanıcı Oluşturma",
                Description = "Yeni kullanıcı oluşturma yetkisi",
                Endpoint = "/api/user",
                Method = "POST"
            },
            new Permission
            {
                Name = "Kullanıcı Güncelleme",
                Description = "Kullanıcı bilgilerini güncelleme yetkisi",
                Endpoint = "/api/user/{id}",
                Method = "PUT"
            },
            new Permission
            {
                Name = "Kullanıcı Silme",
                Description = "Kullanıcı silme yetkisi",
                Endpoint = "/api/user/{id}",
                Method = "DELETE"
            },
            new Permission
            {
                Name = "İzin Listeleme",
                Description = "Tüm izinleri listeleme yetkisi",
                Endpoint = "/api/permission",
                Method = "GET"
            },
            new Permission
            {
                Name = "İzin Detay Görüntüleme",
                Description = "Tek bir iznin detaylarını görüntüleme yetkisi",
                Endpoint = "/api/permission/{id}",
                Method = "GET"
            },
            new Permission
            {
                Name = "İzin Oluşturma",
                Description = "Yeni izin oluşturma yetkisi",
                Endpoint = "/api/permission",
                Method = "POST"
            },
            new Permission
            {
                Name = "İzin Güncelleme",
                Description = "İzin bilgilerini güncelleme yetkisi",
                Endpoint = "/api/permission/{id}",
                Method = "PUT"
            },
            new Permission
            {
                Name = "İzin Silme",
                Description = "İzin silme yetkisi",
                Endpoint = "/api/permission/{id}",
                Method = "DELETE"
            },
            new Permission
            {
                Name = "Kullanıcı İzinlerini Görüntüleme",
                Description = "Kullanıcının izinlerini görüntüleme yetkisi",
                Endpoint = "/api/userpermission/user/{userId}",
                Method = "GET"
            },
            new Permission
            {
                Name = "Kullanıcıya İzin Atama",
                Description = "Kullanıcıya izin atama yetkisi",
                Endpoint = "/api/userpermission",
                Method = "POST"
            },
            new Permission
            {
                Name = "Kullanıcıdan İzin Kaldırma",
                Description = "Kullanıcıdan izin kaldırma yetkisi",
                Endpoint = "/api/userpermission/{id}",
                Method = "DELETE"
            }
        };

        context.Permissions.AddRange(permissions);
        context.SaveChanges();
    }
} 