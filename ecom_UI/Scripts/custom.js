$(document).ready(function () {
$("#imgsizeerror").text("Note : Each File Size must be 1mb or less then 1mb !").attr("class", "text-info")
var i = [];
var myimg = [];
    var pid;
   
//----------- Product Code Start ----------- //


//get product detail from database code
    product_detail();
   
    // update product code
    $("#updatebtn").click(function () {
       
    $("#imgsizeerror").empty();
    var formdata = new FormData();
    $.each(myimg, function (k, v) {
    formdata.append("updateproduct.productimage.img", v)
    formdata.append("updateproduct.productimage.p_id", pid)
    })
       
       
            $.each(i, function (k, pdel) {
                formdata.append("id", pdel)

            })
        
      
    $("#error").empty();
    var id = $("#hdnproductid").val();
    var name = $("#pnametxt").val();
    var description = $("#pdectxt").val();
    var brand = $("#brandtxt").val()
    var price = $("#pricetxt").val();
    var discount_percentage = $("#discount_percentage").val();
    var discounted_price = $("#discounted_price").val();
    var net_price = $("#net_price").val();
    var category = $("#category").val();
    var status = $("#status").val();
    if (name == "" || description == "" || price == "" || discount_percentage == "" || discounted_price == "" || net_price == "" || category == "" || status == "") {
    $("#error").append("<small><b class=text-danger>Please Fill All The Fields !</b></small>")
    }
    else
    {
    formdata.append("updateproduct.product.p_id", id)
    formdata.append("updateproduct.product.product_name", name)
    formdata.append("updateproduct.product.product_description", description)
    formdata.append("updateproduct.product.brand_manfacturer", brand)
    formdata.append("updateproduct.product.price", price)
    formdata.append("updateproduct.product.discount_percentage", discount_percentage)
    formdata.append("updateproduct.product.discounted_price", discounted_price)
    formdata.append("updateproduct.product.net_price", net_price)
    formdata.append("updateproduct.product.sub_category_id", category)
    formdata.append("updateproduct.product.status", status)
    $.ajax({
    type: 'POST',
    url: '/AdminPanel/Product/updateproduct',
    data: formdata,
    contentType: false,
    processData: false,
    success: function (msg)
    {  
    $("#proupdateModal").modal("hide")
    },
    })
        }
       
    })
    $("#proupdateModal").on("hidden.bs.modal", function ()
    {
        $("#data").empty();
        product_detail();
        i.length = 0;
        $("#addmoreimgfile").val("")
        myimg.length=0;
        
    })        
    //Add more image code
    $("#addimg").click(function ()
    {
    $("#addmoreimgfile").trigger("click")
        $("#addmoreimgfile").change(function () {
            
        var img = this.files;
       
    $.each(img, function (k, v) {
    var reader = new FileReader;
    reader.readAsDataURL(v)
    reader.onload = function (x)
    {                    
    var size = v.size;
    if (size <= 1000000)
    {
    $("#imgshow").append("<tr><td><img src='" + x.target.result + "' id=newimg width=50 height=50 title='No Image In this product'/></td></tr>")
   
    pid = $("#hdnproductid").attr("value")
  
    myimg.push(v)
    
    
    $("#imgsizeerror").text("")
    }
    else
    {
    $("#imgsizeerror").text(v.name + "  " + "File Size Is too long !").attr("class", "text-danger")
    }
    }
    })
    $(this).val("")
    })
    })
// calculate discount and net price code
    $("#discount_percentage").focusout(function () {
    var price = $("#pricetxt").val();
    var disc = $("#discount_percentage").val();
    var discprice = (price * disc) / 100;
    $("#discounted_price").val(discprice)
    var netprice = (price - discprice);
    $("#net_price").val(netprice);
    })
    $("#discounted_price").focusin(function () {
    $("#discount_percentage").focus()
    })
    $("#net_price").focusin(function () {
    $("#discount_percentage").focus()
    })
    function product_detail() {
        $.ajax({
            type: 'GET',
            url: '/AdminPanel/Product/get_product_details',
            contentType: false,
            processData: false,
            success: function (data) {
                $.each(data, function (key, value) {
                    $("#data").append("<tr>" +
                        "<td>" + "<h6>" +"#"+ value.p_id + "</h6>" + "</td>" +
                        "<td>" + "<h6>" + value.product_name + "</h6>" + "</td>" +
                        "<td>" + "<h6>" + value.brand_manfacturer + "</h6>" + "</td>"+
                        "<td>" + "<b>" + new Intl.NumberFormat('en-ca', { style: 'currency', currency: 'cad' }).format(value.price) + "</b>" + "</td>" +
                        "<td>" + value.discount_percentage + '.00 %' + "</td>" +
                        "<td>" + "<b>" + new Intl.NumberFormat('en-ca', { style: 'currency', currency: 'cad' }).format(value.discounted_price) + "</td>" +
                        "<td>" + "<b>" + new Intl.NumberFormat('en-ca', { style: 'currency', currency: 'CAD' }).format(value.net_price) + "</td>" +
                        "<td>" +
                        "<i id='editbtn' itemid=" + value.p_id + " data-toggle='modal' data-target='#proupdateModal' class='fa fa-pencil-square text-primary fa-2x'>" + "</i>" + "&nbsp;&nbsp;&nbsp;" +
                        "<i id='deletebtn' itemid=" + value.p_id + "  class='fa fa-trash fa-2x text-danger'>" + "</i>" +
                        "</td>"
                        + "</tr>")
                })
                productdetail()
                editdataselect()
                deleteproduct()
            }
        })
    }
    function productdetail() {
        $("#data #detailbtn").click(function () {
            $("#imgdiv").empty();
            var formdata = new FormData();
            var id = $(this).attr("itemid")
            formdata.append("id", id)
            $.ajax({
                type: 'POST',
                url: '/AdminPanel/Product/get_product_detail_by_id',
                data: formdata,
                contentType: false,
                processData: false,
                success: function (responce) {
                   
                    $.each(responce, function (index, data) {
                        if (data.image_path == null) {
                            data.image_path = "/productimages/noimg.jpg";
                            $("#imgdiv").append("<img src='" + data.image_path + "' height=150 width=150/>");
                        }
                        else {
                            $("#imgdiv").append("<img src='" + data.image_path + "' height=40 width=40/>");
                        }

                        $("#pidlbl").text("Product ID # " + data.p_id)
                        $("#pnamelbl").text("Product Name : " + data.product_name)
                        $("#pdesclbl").text("Product Description : " + data.product_description)
                        $("#brandlbl").text("Brand Manafacturer : " + data.brand_manfacturer)
                        $("#ppricelbl").text("Price : " + data.price)
                        $("#discountpercentagelbl").text("Disc% : " + data.discount_percentage)
                        $("#discountedpricelbl").text("Discounted Price : " + data.discounted_price)
                        $("#netpricelbl").text("Net Price : " + data.net_price)
                        if (data.status == 1) {
                            data.status = "Active"
                            $("#statuslbl").text("Status : " + data.status)
                        }
                        else {
                            data.status = "Deactive"
                            $("#statuslbl").text("Status : " + data.status)
                        }
                        $("#categorylbl").text("Category : " + data.sub_category_name)
                    })
                },
                error: function () {
                    alert("some error" + id)
                }
            })
        })
    }
    function editdataselect() {
        $("#data #editbtn").click(function () {
            $("#error").empty();
            $("#imgshow").empty()
            $("#imgshow #newimg").empty()
            var formdata = new FormData();
            var id = $(this).attr("itemid")
            formdata.append("id", id)
            $.ajax({
                type: 'POST',
                url: '/AdminPanel/Product/get_product_detail_by_id',
                data: formdata,
                contentType: false,
                processData: false,
                success: function (data) {
                    $.each(data, function (k, value) {
                        if (value.image_path == null) {
                            value.image_path = "/productimages/noimg.jpg";
                            $("#imgshow").append("<tr><td><img src='" + value.image_path + "' id=newimg width=100 height=100 title='No Image In this product'/></td></tr>")

                            $("#imgshow #imgdelbtn").hide()
                        }
                        else {
                            $("#imgshow").append("<tr><td><img src='" + value.image_path + "' id=newimg width=50 height=50 title='No Image In this product'/></td><td><button class='badge badge-danger p-1 pull-right' value='" + value.image_id + "' id='imgdelbtn'><i class='fa fa-trash '></i> Delete</button></td></tr>")

                        }

                        $("#hdnproductid").val(value.p_id)
                        $("#pnametxt").val(value.product_name)
                        $("#pdectxt").val(value.product_description + " ")
                        $("#brandtxt").val(value.brand_manfacturer + " ")
                        $("#pricetxt").val(value.price)
                        $("#discount_percentage").val(value.discount_percentage)
                        $("#discounted_price").val(value.discounted_price)
                        $("#net_price").val(value.net_price)
                        $("#category").val(value.sub_cate_id)

                        $("#status").val(value.status)

                    })

                    $("#imgshow #imgdelbtn").click(function () {
                        var tr = $("#imgshow >tr").length;
                        if (tr == 1) {
                            $("#imgshow").append("<tr><td><img src='/productimages/noimg.jpg' id=newimg width=100 height=100 title='No Image In this product'/></td></tr>")
                        }
                        $(this).closest("tr").remove();
                        var id = $(this).attr("value");

                        if (i.includes(id) != true) {
                            i.push(id)
                        }


                    })
                },
                error: function () {
                    console.log("error occured")
                }
            })
        })
    }
    function deleteproduct() {
        $("#data #deletebtn").click(function () {
            var id = $(this).attr("itemid")
            var formdata = new FormData();
            formdata.append("id", id)
            $.ajax({
                type: 'POST',
                url: '/AdminPanel/Product/deleteproduct',
                data: formdata,
                processData: false,
                contentType: false,
                success: function () {
                    $("#data").empty();
                    product_detail()
                }
            })
        })
    }



//----------- Product Code End ----------- //


// -------- Category Code Start ----------- //

    get_category_detail()
    get_sub_category_list()
// add category code 
$("#btnsave").click(function () {
        var cate = $("#catetxt").val()
        var formdata = new FormData();
        formdata.append("catename.category_name", cate)
        $.ajax({
            type:'POST',
            url:'/AdminPanel/Category/create_category',
            data:formdata,
            contentType: false,
            processData: false,
            success: function ()
            {

            }
        })
    })
    // update category code
    $("#savechgbtn").click(function () {
        var id = $("#hdncateid").attr("value")
        var catename = $("#editcatetxt").val()
        var formdata = new FormData();
        formdata.append("catename.category_id",id)
        formdata.append("catename.category_name", catename)
        $.ajax({
            type: 'POST',
            url: '/AdminPanel/Category/update_category',
            data: formdata,
            contentType: false,
            processData: false,
            success: function ()
            {
               
               
                $("#catemodel").hide().fadeOut();
            }




        })

    })
    // add sub category code
    $("#subcatesavebtn").click(function () {
        var subcatename = $("#subcatetxt").val()
        var parentcatename = $("#parentcatenametxt").val()
        var formdata = new FormData();
        formdata.append("sub_catename.sub_category_name", subcatename)
        formdata.append("sub_catename.parent_category_id", parentcatename)
        $.ajax({
            type: 'POST',
            url: '/AdminPanel/Sub_Category/create_sub_category',
            data: formdata,
            contentType: false,
            processData: false,
            success:function ()
            {
                $("#subcatetxt").val("")
                $("#parentcatenametxt").val("")
            }





        })

    })
    $("#catemodel").on('hidden.bs.modal', function () {
        $("#catetbl").empty()
        get_category_detail()
    })
    // update sub category code
    $("#subcateupdatebtn").click(function () {
        var id = $("#hdnsubcateid").val();
        var subcatename = $("#subcateuptxt").val()
        var parentcate = $("#maincateupdrop").val()
        var formdata = new FormData();
        formdata.append("sub_catename.sub_cate_id", id)
        formdata.append("sub_catename.sub_category_name", subcatename)
        formdata.append("sub_catename.parent_category_id", parentcate)
        $.ajax({
            type: 'POST',
            url: '/AdminPanel/Sub_Category/update_sub_category',
            data: formdata,
            contentType: false,
            processData: false,
            success: function () {
                $("#subcatelist").empty()
                get_sub_category_list()
               

            }


        })

    })
    function get_sub_category_list()
    {
        $.ajax({

            type: 'GET',
            url: '/AdminPanel/Sub_Category/get_sub_cate_list',

            contentType: false,
            processData: false,
            success: function (data)
            {
                $.each(data, function (key, value) {
                    $("#subcatelist").append
                        (
                            "<tr>" +
                            "<td>" + value.sub_cate_id +"</td>" +
                            "<td>" + value.sub_category_name + "</td>" +
                            "<td>" + value.category_name + "</td>" +
                            "<td><i class='fa fa-pencil-square-o text-primary fa-2x' data-toggle='modal' data-target='#subcateupdate' itemid='" + value.sub_cate_id + "' id='editsubcatebtn'></i>&nbsp;&nbsp;&nbsp;<i class='fa fa-trash text-danger fa-2x' itemid='" + value.sub_cate_id +"' id='subcatedelbtn'></i></td>"
                            +"</tr>"
                        )

                })
                $("#subcatelist #subcatedelbtn").click(function () {
                    var id = $(this).attr("itemid")
                    var formdata = new FormData()
                    formdata.append("id", id)
                    $.ajax({
                        type: 'POST',
                        url: '/AdminPanel/Sub_Category/delete_sub_category',
                        data: formdata,
                        contentType: false,
                        processData: false,
                        success: function () {
                            $("#subcatelist").empty()
                            get_sub_category_list()


                        }


                    })
                    


                })
                $("#subcatelist #editsubcatebtn").click(function () {
                
                    var id = $(this).attr("itemid")
                    $("#hdnsubcateid").val(id);
                    var formdata = new FormData();
                    formdata.append("id", id)
                    $.ajax({
                        type: 'POST',
                        url: '/AdminPanel/Sub_Category/get_sub_category_by_id',
                        data: formdata,
                        contentType: false,
                        processData: false,
                        success: function (data)
                        {
                            $.each(data, function (key, value) {
                                console.log(value)
                                $("#subcateuptxt").val(value.sub_category_name)
                                $("#maincateupdrop").val(value.parent_category_id)
                                
                            })
                           
                           
                        }


                    })

                })
            }


        

        })
}
    function get_category_detail()
    {
        $.ajax({

            type: 'GET',
            url: '/AdminPanel/Category/get_category',

            contentType: false,
            processData: false,
            success: function (data) {
                $.each(data, function (key, value) {
                    $("#catetbl").append
                        ("<tr>" +
                            "<td>" + value.category_id + "</td>"+
                        "<td>" + value.category_name + "</td>" +
                        "<td><i class='fa fa-pencil-square-o text-primary fa-2x' id='editbtn' data-toggle='modal' data-target='#catemodel' itemid='" + value.category_id + "'></i>&nbsp;&nbsp;&nbsp;<i class='fa fa-trash text-danger fa-2x' itemid='" + value.category_id+"' id='deletecatebtn'></i></td>"+
                            +"<tr/>")

                })
                $("#catetbl #editbtn").click(function () {
                    $tr = $(this).closest("tr");
                    var data = $tr.children("td").map(function () {
                        return $(this).text();
                    }).get()

                    $("#editcatetxt").val(data[1])
                    $("#hdncateid").attr("value", data[0])
                })
                // delete category code
                $("#catetbl #deletecatebtn").click(function () {
                    $tr = $(this).closest("tr");
                    var data = $tr.children("td").map(function () {
                        return $(this).text();
                    }).get()
                   $("#hdncateid").attr("value", data[0])
                    var id = $("#hdncateid").attr("value")
                    var formdata = new FormData();
                    formdata.append("id", id)
                    $.ajax({
                        type: 'POST',
                        url: '/AdminPanel/Category/delete_category',
                        data: formdata,
                        contentType: false,
                        processData: false,
                        success: function ()
                        {
                            $("#catetbl").empty()
                            get_category_detail()
                        }




                    })



                })

            }



        })
    }

// ----------- Category Code End ----------- //

    // ----------- User Code start ----------- //
    user_list()
    var img;
    var uid;
    var role_id;
    var user_img_id;
    var userrole_id;
    var oldimg;
    $("#userimage").attr("src", "/productimages/noimg.jpg")
    //Choose photo code
    $("#choosephotobtn").click(function () {
        $("#filedialog").trigger("click")
        $("#filedialog").change(function () {
            img = this.files[0]
           
            var reader = new FileReader();
            reader.readAsDataURL(img)
            reader.onload = function (x)
            {
                $("#userimage").attr("src", x.target.result)
               
            }





        })
        

    })
    // user save code
    $("#usersavebtn").click(function () {
        var username = $("#usernametxt").val()
        var password = $("#passwordtxt").val()
        var confirmpassword = $("#confirmpaswordtxt").val()
        var email = $("#emailtxt").val()
        var contact = $("#contacttxt").val()
        var role = $("#roledropdown").val()
        var status = $("#userstatusdropdown").val()
        var formdata = new FormData();
        formdata.append("cus_user_role_img.user.user_name",username);
        formdata.append("cus_user_role_img.user.password",password);
        formdata.append("cus_user_role_img.user.confirm_password",confirmpassword);
        formdata.append("cus_user_role_img.user.email_address",email);
        formdata.append("cus_user_role_img.user.contact_no",contact);
        formdata.append("cus_user_role_img.user_role.role_id",role);
        formdata.append("cus_user_role_img.user.user_status",status);
        formdata.append("cus_user_role_img.user_image.userimg",img);
     
        $.ajax({
            type: 'POST',
            url: '/AdminPanel/User/insert_user',
            data: formdata,
            contentType: false,
            processData: false,
            success: function () {
                $("#usernametxt").val("")
                $("#passwordtxt").val("")
                $("#confirmpaswordtxt").val("")
                $("#emailtxt").val("")
                $("#contacttxt").val("")
                $("#roledropdown").val("")
                $("#userstatusdropdown").val("")
                $("#userimage").attr("src", " /productimages/noimg.jpg")
            }
        })


    })
    //delete user code
    function delete_user()
    {
        $("#userlist #userdeletebtn").click(function () {
            var id = $(this).attr("itemid")
            var formdata = new FormData();
            formdata.append("id", id)
            $.ajax({
                type: 'POST',
                url: '/AdminPanel/User/delete_user',
                data: formdata,
                contentType: false,
                processData: false,
                success: function () {
                    $("#userlist").empty()
                    user_list();
                  
                }
            })
        })
    }
    // update user code
    $("#editusersavebtn").click(function () {
        var username = $("#usernametxt").val()
        var password = $("#passwordtxt").val()
        var confirmpassword = $("#confirmpaswordtxt").val()
        var email = $("#emailtxt").val()
        var contact = $("#contacttxt").val()
        var role = $("#roledropdown").val()
        var status = $("#userstatusdropdown").val()
        var role = $("#userrolelist").val()
        var formdata = new FormData();
        formdata.append("cus_user_role_img.user.user_name", username);
        formdata.append("cus_user_role_img.user.password", password);
        formdata.append("cus_user_role_img.user.confirm_password", confirmpassword);
        formdata.append("cus_user_role_img.user.email_address", email);
        formdata.append("cus_user_role_img.user.contact_no", contact);
        formdata.append("cus_user_role_img.user_role.role_id", role);
        formdata.append("cus_user_role_img.user.user_status", status);
        if (oldimg != null) {
            formdata.append("cus_user_role_img.user_image.image_path", oldimg);
        }
        else
        {
            formdata.append("cus_user_role_img.user_image.userimg", img);
        }
        formdata.append("cus_user_role_img.user.user_id", uid);
        formdata.append("cus_user_role_img.user_image.user_img_id", user_img_id);
        formdata.append("cus_user_role_img.user_role.userrole_id", userrole_id);
        $.ajax({
            type: 'POST',
            url: '/AdminPanel/User/update_user',
            data: formdata,
            contentType: false,
            processData: false,
            success: function () {
                $("#userlist").empty()
                user_list();
            }
        })
    })
    // user list code
    function user_list()
    {
        $.ajax({
            type: 'GET',
            url: '/AdminPanel/User/get_user_list',
            contentType: false,
            processData: false,
            success: function (userdata) {
                $.each(userdata, function (key, value) {
                    $("#userlist").append
                        (
                            "<tr>" +
                            "<td>" + "<h6>" +"# "+value.user_id + "</h6>"+"</td>" +
                            "<td>" + "<h6>"+value.user_name+"</h6>" + "</td>" +
                            "<td>" + "<h6>"+value.password+"</h6>" + "</td>" +
                            "<td>" + "<h6>"+value.email_address+"</h6>" + "</td>" +
                            "<td>" + "<h6>"+value.contact_no+"</h6>" + "</td>" +
                            "<td><i class='fa fa-info-circle fa-2x text-success' data-toggle='modal' data-target='#userdetailModal' id='userdetailbtn' itemid='" + value.user_id + "'></i>&nbsp;&nbsp;&nbsp;<i class='fa fa-pencil-square text-primary fa-2x' data-toggle='modal' data-target='#userdetail_for_update_Modal' id='usereditbtn' itemid='" + value.user_id + "'></i>&nbsp;&nbsp;&nbsp;<i  class='fa fa-trash fa-2x text-danger' id='userdeletebtn'  itemid='" + value.user_id +"'></i></td>"+
                            "</tr>"
                        )
                })
                user_detail()
                user_data_select_for_update()
                delete_user()

            }
        })
    }
    //user full detail code
    function user_detail() {
        $("#userlist #userdetailbtn").click(function () {
            $("#userinfo").empty()
            var id = $(this).attr("itemid")
            var formdata = new FormData();
            formdata.append("id", id)
            $.ajax({
                type: 'POST',
                url: '/AdminPanel/User/full_detail_of_user',
                data: formdata,
                contentType: false,
                processData: false,
                success: function (userinfo) {
                    $.each(userinfo, function (key, value) {
                        if (value.image_path == null) {
                            $("#userimg").attr("src", "/productimages/noimg.jpg")
                        }
                        else {
                            $("#userimg").attr("src", value.image_path)
                        }

                        $("#user_id").text("User ID# " + value.user_id);
                        $("#user_name").text("Username: " + value.user_name);
                        $("#password").text("Password: " + value.password);
                        $("#confirm_password").text("Confirm Password: " + value.confirm_password);
                        $("#email_address").text("Email: " + value.email_address);
                        $("#contact_no").text("Contact Us: " + value.contact_no);
                        if (value.user_status == 0) {
                            value.user_status = "Deactive"

                        }
                        else {
                            value.user_status = "Active"
                        }
                        $("#user_status").text("Status: " + value.user_status);
                        $("#user_role").text("Role: " + value.user_role);

                    })


                }
            })



        })
    }
    // user_data_select_for_update
    function user_data_select_for_update() {
        $("#userlist #usereditbtn").click(function () {
            var id = $(this).attr("itemid")
            var formdata = new FormData();
            formdata.append("id", id)
            $.ajax({
                type: 'POST',
                url: '/AdminPanel/User/full_detail_of_user',
                data: formdata,
                contentType: false,
                processData: false,
                success: function (userinfo_for_update) {
                    $.each(userinfo_for_update, function (key, data) {
                        if (data.image_path == null) {
                            $("#upuserimg").attr("src", "/productimages/noimg.jpg")
                        }
                        else {
                            $("#upuserimg").attr("src", data.image_path)
                        }
                        $("#usernametxt").val(data.user_name)
                        $("#passwordtxt").val(data.password)
                        $("#confirmpaswordtxt").val(data.confirm_password)
                        $("#emailtxt").val(data.email_address)
                        $("#contacttxt").val(data.contact_no)
                        $("#userrolelist").val(data.role_id)
                        $("#userstatusdropdown").val(data.user_status)
                        uid = data.user_id
                        role_id = data.role_id
                        user_img_id = data.user_img_id
                        oldimg = data.image_path
                        userrole_id = data.userrole_id
                    })
                }
            })
        })
    }
    //Choose photo edit code
    $("#editchoosephotobtn").click(function () {
        $("#editfiledialog").trigger("click")
        $("#editfiledialog").change(function () {
            oldimg = null;
            img = this.files[0]
            var reader = new FileReader();
            reader.readAsDataURL(img)
            reader.onload = function (x) {
                $("#upuserimg").attr("src", x.target.result)
            }
        })
    })
    // ----------- User Code End ----------- //

      // ----------- Role Code Start ----------- //
    var roleid;
   
    get_role_list()
    // GET ROLE LIST CODE
    function get_role_list()
    {
        $.ajax({
            type: 'GET',
            url: '/AdminPanel/Role/get_role',
            contentType: false,
            processData: false,
            success: function (roledata) {
                $.each(roledata, function (key, value) {
                    $("#rolelist").append
                        (
                            "<tr>" +
                            "<td>" + "<h6>" + "# " + value.role_id + "</h6>" + "</td>" +
                            "<td>" + "<h6>" + value.user_role + "</h6>" + "</td>" +
                            "<td><i class='fa fa-pencil-square text-primary fa-2x' data-toggle='modal' data-target='#roleeditmodel' id='editrolebtn' itemid='" + value.role_id + "'></i>&nbsp;&nbsp;&nbsp;<i  class='fa fa-trash fa-2x text-danger' id='deleterolebtn'  itemid='" + value.role_id + "'></i></td>" +
                            "</tr>"
                        )
                })
                update_data_select()
                delete_role()
            }
        })
    }
    // ADD ROLE CODE
    $("#rolesavebtn").click(function () {
        var rolename = $("#roletxt").val()
        var formdata = new FormData();
        formdata.append("role.user_role", rolename)
        $.ajax({
            type: 'POST',
            url: '/AdminPanel/Role/insert_role',
            data: formdata,
            contentType: false,
            processData: false,
            success: function () {

            }


        })


    })
    // UPDATE DATA SELECT ROLE CODE
    function update_data_select()
    {
        $("#rolelist #editrolebtn").click(function () {
            $tr = $(this).closest("tr")
            var data = $tr.children("td").map(function () {
                return $(this).text()
            }).get()
            roleid = $(this).attr("itemid")
            console.log(roleid)
            $("#editroletxt").val(data[1])



        })
    }
    // UPDATE ROLE CODE
    $("#roleupdatebtn").click(function () {
        var rolename = $("#editroletxt").val()
        var formdata = new FormData();
        formdata.append("role.role_id", roleid)
        formdata.append("role.user_role", rolename)
        $.ajax({
            type: 'POST',
            url: '/AdminPanel/Role/update_role',
            data: formdata,
            contentType: false,
            processData: false,
            success: function () {
                $("#rolelist").empty()
                get_role_list()
            }


        })



    })
    // Delete ROLE CODE
    function delete_role()
    {
        $("#rolelist #deleterolebtn").click(function () {
            var id = $(this).attr("itemid")
            var formdata = new FormData();
            formdata.append("id", id)
           
            $.ajax({
                type: 'POST',
                url: '/AdminPanel/Role/delete_role',
                data: formdata,
                contentType: false,
                processData: false,
                success: function () {
                    $("#rolelist").empty()
                    get_role_list()
                }


            })




        }) 

    }
    // ----------- Role Code Start ----------- //



    // ----------- ORDER Code Start ----------- //
    get_order()
    //GET ORDER DETAIL CODE
    function get_order() {
        $.ajax({
            type: 'GET',
            url: '/AdminPanel/Order/get_order',
            contentType: false,
            processData: false,
            success: function (order) {
                $.each(order, function (key, value) {
               
                    $("#orderlist").append
                        (

                            "<tr>" +
                            "<td>" + "<b>" + "# " + value.order_no + "</b>" + "</td>" +
                            "<td>" + "<b>" + value.order_date_and_time + "</b>" + "</td>" +
                            "<td>" + "<b>" + new Intl.NumberFormat('en-ca', { style: 'currency', currency: 'CAD' }).format(value.net_total) + "</b>" + "</td>" +
                            "<td>" + "<b>" + value.payment_type + "</b>" + "</td>" +
                            "<td>" + "<b id='statusclr'>" + value.order_status + "</b>" + "</td>" +
                            "<td><i class='fa fa-info-circle fa-2x text-success' data-toggle='modal' data-target='#ordermodel' id='orderdetailbtn' itemid='" + value.order_no + "'></i>&nbsp;&nbsp;&nbsp;<i  class='fa fa-trash fa-2x text-danger' id='orderdeletebtn'  itemid='" + value.order_no + "'></i></td>" +
                            "</tr>"
                        )

                })
                delete_order()
                order_info_by_id()
               
            }
        })
    }

    // ORDER INFO BY ID CODE
    function order_info_by_id()
    {
        $("#orderlist #orderdetailbtn ").click(function () {
            $("#ordertbllist").empty()
            var id = $(this).attr("itemid")
            var formdata = new FormData()
            formdata.append("id", id)
            $.ajax({
                type: 'POST',
                url: '/AdminPanel/Order/get_order_by_id',
                data: formdata,
                contentType: false,
                processData: false,
                success: function (order_by_id)
                {
                    $.each(order_by_id, function (key, value) {
                        $("#orshow").text(value.order_no)
                        $("#ordateshow").text(value.order_date_and_time)
                        $("#ordertbllist").append
                            (
                                "<tr>" +
                                "<td>" + value.product_name + "</td>" +
                                "<td>" + value.qty + ".00" + "</td>" +
                                "<td>"+"<b>" + new Intl.NumberFormat('en-ca', { style: 'currency', currency: 'cad' }).format(value.rate) + "</b>"+"</td>" +
                                "<td>" + "<b>" + new Intl.NumberFormat('en-ca', { style: 'currency', currency: 'cad' }).format(value.total)+"</b>"+ "</td>" +
                                "<tr/>"
                            )
                        $("#discshow").text(value.discount_percentage)
                        $("#discpriceshow").text(new Intl.NumberFormat('en-ca', { style: 'currency', currency: 'cad' }).format(value.discounted_price))
                        $("#gstshow").text(value.gst)
                        $("#nettotalshow").text(new Intl.NumberFormat('en-ca', { style: 'currency', currency: 'cad' }).format(value.net_total))
                        $("#paymenttypeshow").text(value.payment_type)                     
                        $("#orderstatusshow").val(value.order_status)

                        $("#ctry").text(value.country)
                        $("#city").text(value.city)
                        $("#addrs").text(value.address)
                        $("#zip").text(value.zip_or_postal_code)
                        $("#pnane").text(value.order_person_name)
                        $("#cno").text(value.contact_no)
                        discprice = value.discounted_price;
                        nettotal = value.net_total
                        addrs_id = value.o_addrs_id
                    })
                   
                }
               
            })
        })
       
    }
    var discprice;
    var nettotal;
    // ORDER UPDATE CODE
      $("#saveorderbtn").click(function () {
            var order_id = $("#orshow").text()
            var orderdate = $("#ordateshow").text()
            var discper = $("#discshow").text()
            var gst = $("#gstshow").text()          
            var paytype = $("#paymenttypeshow").text()
            var orderstatus = $("#orderstatusshow").val()
            var formdata = new FormData()
            formdata.append("order.order_no", order_id)
            formdata.append("order.order_date_and_time", orderdate)
            formdata.append("order.discount_percentage", discper)
            formdata.append("order.discounted_price", discprice)
            formdata.append("order.gst", gst)
            formdata.append("order.net_total", nettotal)
            formdata.append("order.payment_type", paytype)
            formdata.append("order.order_status", orderstatus)
            $.ajax({
                type: 'POST',
                url: '/AdminPanel/Order/order_update',
                data: formdata,
                contentType: false,
                processData: false,
                success: function () {
                    $("#orderlist").empty()
                    get_order()
                }
            })
        })
    //ORDER DELETE CODE
    function delete_order()
    {
        $("#orderlist #orderdeletebtn").click(function () {
         
            var id = $(this).attr("itemid")
            var addrsid = addrs_id;
            var formdata = new FormData();
            formdata.append("id", id)
            formdata.append("addrsid", addrsid)
          
            $.ajax({
                type: 'POST',
                url: '/AdminPanel/Order/delete_order',
                data: formdata,
                contentType: false,
                processData: false,
                success: function () {
                    $("#orderlist").empty()
                    get_order()
                }


            })


        })

    }
    
    
   

      // ----------- ORDER Code END ----------- //
    
})


