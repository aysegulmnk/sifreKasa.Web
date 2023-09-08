$("#registerForm").submit(function (event) {
    event.preventDefault(); // Formun normal gönderimini engelle

    var formData = {
        username: $("#username").val(),
        password: $("#password").val(),
        email: $("#email").val(),
        firstName: $("#firstName").val(),
        lastName: $("#lastName").val()
    };

    $.ajax({
        type: "POST",
        url: "/Account/Register", // Sunucu tarafındaki kayıt işlemi için uygun URL'yi ayarlayın
        data: JSON.stringify(formData),
        contentType: "application/json",
        success: function (response) {
            // Kayıt işlemi başarılı olduysa burada bir işlem yapabilirsiniz
            alert("Kayıt işlemi başarıyla tamamlandı.");
        },
        error: function (error) {
            // Kayıt işlemi başarısız olduysa burada bir işlem yapabilirsiniz
            alert("Kayıt işlemi sırasında bir hata oluştu.");
        }
    });
});
