$("#registerForm").submit(function (event) {
    event.preventDefault();

    var formData = {
        username: $("#username").val(),
        password: $("#password").val(),
        email: $("#email").val(),
        firstName: $("#firstName").val(),
        lastName: $("#lastName").val()
    };

    $.ajax({
        type: "POST",
        url: "/Account/Register", 
        data: JSON.stringify(formData),
        contentType: "application/json",
        success: function (response) {
            
            alert("Kayıt işlemi başarıyla tamamlandı.");
        },
        error: function (error) {
            
            alert("Kayıt işlemi sırasında bir hata oluştu.");
        }
    });
});
