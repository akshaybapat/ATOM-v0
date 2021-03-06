﻿
var current_fs, next_fs, previous_fs;

$.fn.scrollTo = function (target, options, callback) {
  if (typeof options == 'function' && arguments.length == 2) { callback = options; options = target; }
  var settings = $.extend({
    scrollTarget: target,
    offsetTop: 50,
    duration: 500,
    easing: 'swing'
  }, options);
  return this.each(function () {
    var scrollPane = $(this);
    var scrollTarget = (typeof settings.scrollTarget == "number") ? settings.scrollTarget : $(settings.scrollTarget);
    var scrollY = (typeof scrollTarget == "number") ? scrollTarget : scrollTarget.offset().top + scrollPane.scrollTop() - parseInt(settings.offsetTop);
    scrollPane.animate({ scrollTop: scrollY }, parseInt(settings.duration), settings.easing, function () {
      if (typeof callback == 'function') { callback.call(this); }
    });
  });
}

function hello() {
  $('#liLogout').show();
}

function gotoURL(url) {

  var xhr = new XMLHttpRequest();

  xhr.onreadystatechange = function () {
    if (xhr.readyState == 4 && xhr.status == 200) {

      $('#bigblue').show();

      window.location.href = url;
    }


  }

  xhr.open('head', url);
  xhr.send(null);
  $('#bigblue').hide();

}


$('#KeySite').change(function () {
 
  var selectedSite = '';
  selectedSite = $('#KeySite option:selected').text()

  $.ajax({
    type: 'POST',
    url: $('#getDropdownActionUrl').data('url'),
    //data: { typeofData: "BuildingList", filter: $('#FacilitiesID option:selected').text() },
    data: { filter: selectedSite },
    success: function (data) {
      var items = '<option>Select a Building</option>';

      $.each(data, function (i, building) {
        items += "<option value='" + building.id + "'>" + building.BuildingName + '</option>';
      });
      $('#KeyBuilding').html(items);
    }
  });
});

function getAvailableCustomers() {
  var selection = '';
  //selection = $('#FacilitiesID option:selected').text()
  selection=$('#lblSiteName').text();
  var availablecustomers = '';

  //$.getJSON('/FFSite/GetDropDownData', { typeofData: "CustomersList", filter: selection }, function (data) {


  //  var items = '<option>Select a FlexFlow Instance</option>';

  //  $.each(data, function (i, businesspartner) {
  //    items += "<option value='" + businesspartner.id + "'>" + businesspartner.BPCode + "</option>";

  //    availablecustomers += '<li><div id="selectedFFInstance">' + businesspartner.BPCode + '</div><div id="selectedFFInstanceID" style="display:none;">' + businesspartner.id + '</div></li>';
  //  });

  //  //if (availablecustomers.length < 2) availablecustomers = '<tr><td><h4><b>Please Select a Valid FlexFlow Instance</b></h4></td></tr>';

  //  $('#availablecustomersresultset').html(availablecustomers);

  //  $(document).bind('selectstart dragstart', function (e) {
  //    e.preventDefault(); return false;
  //  });


  //});

  $.ajax({
    type: 'POST',
    url: $('#getDropdownActionUrl').data('url'),
    data: { typeofData: "CustomersList", filter: selection },
    success: function (data) {
      var items = '<option>Select a FlexFlow Instance</option>';

      $.each(data, function (i, businesspartner) {
        items += "<option value='" + businesspartner.id + "'>" + businesspartner.BPCode + "</option>";

        availablecustomers += '<li><div id="selectedFFInstance">' + businesspartner.BPCode + '</div><div id="selectedFFInstanceID" style="display:none;">' + businesspartner.id + '</div></li>';
      });

      //if (availablecustomers.length < 2) availablecustomers = '<tr><td><h4><b>Please Select a Valid FlexFlow Instance</b></h4></td></tr>';

      $('#availablecustomersresultset').html(availablecustomers);

      $(document).bind('selectstart dragstart', function (e) {
        e.preventDefault(); return false;
      });
    }
  });
}

function getAssignedCustomers() {
  var assignedcustomers = '';
  //$.getJSON('/FFSite/GetDropDownData', { typeofData: "CustomersList", buildingfilter: $('#BuildingsID option:selected').text() }, function (data) {

  //  var items = '<option>---SELECT---</option>';
  //  $.each(data, function (i, businesspartner) {
  //    items += "<option value='" + businesspartner.id + "'>" + businesspartner.BPCode + "</option>";

  //    assignedcustomers += '<li><div id="selectedFFInstance" >' + businesspartner.BPCode + '</div><div id="selectedFFInstanceID" style="display:none;">' + businesspartner.id + '</div></li>';
  //  });
  //  $('#assignedcustomersresultset').html(assignedcustomers);
  //});
  $.ajax({
    type: 'POST',
    url: $('#getDropdownActionUrl').data('url'),
    data: { typeofData: "CustomersList", buildingfilter: $('#BuildingsID option:selected').text() },
    success: function (data) {
      var items = '<option>---SELECT---</option>';
      $.each(data, function (i, businesspartner) {
        items += "<option value='" + businesspartner.id + "'>" + businesspartner.BPCode + "</option>";

        assignedcustomers += '<li><div id="selectedFFInstance" >' + businesspartner.BPCode + '</div><div id="selectedFFInstanceID" style="display:none;">' + businesspartner.id + '</div></li>';
      });
      $('#assignedcustomersresultset').html(assignedcustomers);

    }
  });
}

$('#rightnavigation').on('click', function (event) {
  $('#ffinstance').scrollLeft(2000)

});

$('#leftnavigation').on('click', function (event) {
  $('#ffinstance').scrollLeft(-2000)

});

$('table tr').on('click', function (e) {

  var state = $(this).hasClass('highlight');

  /*Reset field values and Next Button */
  $('.highlight').removeClass('highlight');

  if (!state) {
    $(this).addClass('highlight');
  }
});



//$('#SearchBox').on("keyup paste", function () {

//  var value = $(this).val().toUpperCase();
//  var $rows = $("table tr");

//  if (value === '') {
//    $rows.show();
//    return false;
//  }

//  $rows.each(function (index) {
//    if (index !== 0) {

//      $row = $(this);

//      var column1 = $row.find("td").eq(0).text().toUpperCase();
//      var column2 = $row.find("td").eq(1).text().toUpperCase();

//      if ((column1.indexOf(value) > -1) || (column2.indexOf(value) > -1)) {
//        $row.show();
//      }
//      else {
//        $row.hide();
//      }
//    }
//  });

//});



$('#CountriesDivID').hide();
$('#FacilitiesDivID').hide();
$('#Next-ToBuildingSelection').hide();


$('#msform').load(function () {
 
});


$('#RegionID').load(function () {
 
  //$.getJSON('/FFSite/GetDropDownData', { typeofData: "RegionList" }, function (data) {
  //  var items = '<option>Select a Country</option>';
  //  $.each(data, function (i, region) {
  //    items += "<option value='" + region.Value + "'>" + region.Text + "</option>";
  //  });
  //  $('#RegionID').html(items);
  //  $('#RegionID').show();

  //});



  $.ajax({
    type: 'POST',
    url: $('#getDropdownActionUrl').data('url'),
    data: { typeofData: "RegionList" },
    success: function (data) {
      var items = '<option>Select a Country</option>';
      $.each(data, function (i, region) {
        items += "<option value='" + region.Value + "'>" + region.Text + "</option>";
      });
      $('#RegionID').html(items);
      $('#RegionID').show();
    }
  });






});

$('#RegionID').change(function () {

  $('#CountriesDivID').hide();
  $('#FacilitiesDivID').hide();

  if ($('#RegionID').selectedIndex <= 0 || $('#RegionID').is(":hidden") || $('#CountriesID').selectedIndex <= 0 || $('#CountriesID').is(":hidden") || $('#FacilitiesID').selectedIndex <= 0 || $('#FacilitiesID').is(":hidden"))
    $('#Next-ToBuildingSelection').hide();


  //$.getJSON('/FFSite/GetDropDownData', { typeofData: "CountryList", filter: $('#RegionID').val() }, function (data) {
  //    var items = '<option>---SELECT---</option>';
  //    $.each(data, function (i, country) {
  //        items += "<option value='" + country.id + "'>" + country.CountryName + "</option>";
  //    });
  //    $('#CountriesID').html(items);
  //    $('#CountriesDivID').show();

  //});

  $.ajax({
    type: 'POST',
    url: $('#getDropdownActionUrl').data('url'),
    data: { typeofData: "CountryList", filter: $('#RegionID').val() },
    success: function (data) {
      var items = '<option>---SELECT---</option>';
      $.each(data, function (i, country) {
        items += "<option value='" + country.id + "'>" + country.CountryName + "</option>";
      });
      $('#CountriesID').html(items);
      $('#CountriesDivID').show();
    }
  });
});

$('#CountriesID').change(function () {

  $('#FacilitiesDivID').hide();

  if ($('#RegionID').selectedIndex <= 0 || $('#RegionID').is(":hidden") || $('#CountriesID').selectedIndex <= 0 || $('#CountriesID').is(":hidden") || $('#FacilitiesID').selectedIndex <= 0 || $('#FacilitiesID').is(":hidden"))
    $('#Next-ToBuildingSelection').hide();


  //$.getJSON('/FFSite/GetDropDownData', { typeofData: "FacilityList", filter: $('#CountriesID option:selected').text() }, function (data) {
  //    var items = '<option>---SELECT---</option>';
  //    $.each(data, function (i, facility) {
  //        items += "<option value='" + facility.id + "'>" + facility.SiteName + "</option>";
  //    });
  //    $('#FacilitiesID').html(items);
  //    $('#FacilitiesDivID').show();

  //});

  $.ajax({
    type: 'POST',
    url: $('#getDropdownActionUrl').data('url'),
    data: { typeofData: "FacilityList", filter: $('#CountriesID option:selected').text() },
    success: function (data) {
      var items = '<option>---SELECT---</option>';
      $.each(data, function (i, facility) {
        items += "<option value='" + facility.id + "'>" + facility.SiteName + "</option>";
      });
      $('#FacilitiesID').html(items);
      $('#FacilitiesDivID').show();
    }
  });




});

$('#FacilitiesID').change(function () {

  if ($('#RegionID').selectedIndex <= 0 || $('#RegionID').is(":hidden") || $('#CountriesID').selectedIndex <= 0 || $('#CountriesID').is(":hidden") || $('#FacilitiesID').selectedIndex <= 0 || $('#FacilitiesID').is(":hidden"))
    $('#Next-ToBuildingSelection').hide();
  else
    $('#Next-ToBuildingSelection').show();
});


$('#Next-ToBuildingSelection').on('click', function () {

  $('#Next-ToBucketSelection').hide();
  $('#availablecustomersresultset').html("");
  $('#assignedcustomersresultset').html("");

  var siteName = $('#lblSiteName').text();

  $.ajax({
    type: 'POST',
    url: $('#getDropdownActionUrl').data('url'),
    data: { typeofData: "BuildingList", filter: siteName },
    success: function (data) {
      var items = '<option>Select a Building</option>';

      $.each(data, function (i, building) {
        items += "<option value='" + building.id + "'>" + building.BuildingName + '</option>';
      });
      $('#BuildingsID').html(items);
    }
  });

  $('#EditBuilding').click(function () {
    if ($('#BuildingsID option:selected').val() !== null) {
      var url = $('#DimBuildingsEditUrl').data('url') + "?id=" + $('#BuildingsID option:selected').val();
      gotoURL(url);
    }
  });

  $('#DetailsBuilding').click(function () {
    if ($('#BuildingsID option:selected').val() !== null) {
      var url = $('#DimBuildingsDetailsUrl').data('url') + "?id=" + $('#BuildingsID option:selected').val();
      gotoURL(url);
    }
  });

  $('#DeleteBuilding').click(function () {
    if ($('#BuildingsID option:selected').val() !== null) {
      var url = $('#DimBuildingsDeleteUrl').data('url') + "?id=" + $('#BuildingsID option:selected').val();
      gotoURL(url);
    }
  });

  getAvailableCustomers();

  $('#BuildingsID').change(function () {
    
    var building = $('#BuildingsID option:selected').text();

    /*Display Next Button */
    if (building == "Select a Building") {
      $('#Next-ToBucketSelection').hide();
      $('#assignedcustomersresultset').html('');
      return false;
    }
    else {
      $('#Next-ToBucketSelection').show();
    }

    getAssignedCustomers();

    getAvailableCustomers();

    $("#progressbar li").eq(1).text("Building Selection");

    var building = $('#BuildingsID option:selected').text();

    $("#progressbar li").eq(1).append("<br/> { " + building + " }");

    console.log($('#BuildingsID option:selected').val() == 'Select a Building')
  });


  var jsonAssignedCustomers = [];
  var jsonAvailableCustomers = [];

  $('#SaveButton').click(function () {
    if ($('#BuildingsID option:selected').val() == 'Select a Building')
    {
      alert("Please select at least one building")
      return false;
    }

    $('#fieldBulding').prop('disabled', 'disabled');
  
    var assignedcustomers = $("#assignedcustomersresultset li");

    jsonAssignedCustomers = [];

    $.each(assignedcustomers, function (i, customer) {
      var row = $(customer).find("#selectedFFInstance").text()
      jsonAssignedCustomers.push(row);
      console.log(row);
    });

    var availablecustomers = $("#availablecustomersresultset li");

    jsonAvailableCustomers = [];

    $.each(availablecustomers, function (i, customer) {
      var row = $(customer).find("#selectedFFInstance").text()
      jsonAvailableCustomers.push(row);
      console.log(row);
    });

    var ffInstanceAJAXModel = {
      buildingname: $('#BuildingsID option:selected').text(),
      assignedcustomers: jsonAssignedCustomers,
      availablecustomers: jsonAvailableCustomers
    }

    $.ajax({
      type: 'POST',
      url: $('#DimBusinessPartnersUpdateUrl').data('url'),
      data: JSON.stringify(ffInstanceAJAXModel),
      contentType: 'application/json',
      dataType: 'json',
      sucess: function (result) {
        $('#bigblue').hide()
      }
    });

    $.ajax({
      type: 'POST',
      url: $('#DimFFInstancesUpdateUrl').data('url'),
      data: JSON.stringify(ffInstanceAJAXModel),
      success: function (data) {
        alert("Saved successfully")
        $('#fieldBulding').prop('disabled', false);
      }
    });
  });
});


$('#Next-ToBucketSelection').on('click', function () {
  var building = $('#BuildingsID option:selected').text();
  var customer = '';

  $('#stationtypesresultset').html('');
  $('#bucketedstationtypesresultset').html('');

  $('#divBuildingName').html(building);
  $.ajax({
    type: 'POST',
    url: $('#getDropdownActionUrl').data('url'),
    data: { typeofData: "CustomersList", buildingfilter: building },
    success: function (data) {
      $('#BucketsDropDown').prop('disabled', 'disabled');
      var items = '<option>Select a Customer Project</option>';
      $.each(data, function (i, customer) {
        items += "<option value='" + customer.id + "'>" + customer.BPCode + "</option>";
      });
      $('#CustomersDropDown').html(items);
    }
  });

  $.ajax({
    type: 'POST',
    url: $('#getDropdownActionUrl').data('url'),
    data: { typeofData: "BucketsList" },
    success: function (data) {
      var items = '<option>Select a Bucket Type</option>';
      $.each(data, function (i, bucket) {
        items += "<option value='" + bucket.id + "'>" + bucket.BucketName + "</option>";
      });
      $('#BucketsDropDown').html(items);
    }
  });

  $('#CustomersDropDown').change(function () {
  
    customer = $('#CustomersDropDown option:selected').text();
    if (customer == "Select a Customer Project")
    {
      $('#stationtypesresultset').html('');
      return false;
    }
    
    $.ajax({
      type: 'POST',
      url: $('#getDropdownActionUrl').data('url'),
      data: { typeofData: "StationTypesList", filter: customer },
      success: function (data) {
        var result = '';
        $('#BucketsDropDown').prop('disabled', false);
        var items = '<option>Select a Station Type</option>';

        $.each(data, function (i, stationtype) {
          items += "<option value='" + stationtype.id + "'>" + stationtype.StationTypeName + "</option>";

          result += '<li><div id="selectedStationTypeName" style="border-radius:15px;">' + stationtype.StationTypeName + '</div><div id="selectedStationTypeID" style="display:none;">' + stationtype.id + '</div></li>';
        });

        $('#stationtypesresultset').html(result);

        /* Get all rows from your 'table' but not the first one 
        * that includes headers. */
        var row = '';
        var selectedFFInstanceID = '';
        var rows = $('#stationtypesresultset li');

        $(document).bind('selectstart dragstart', function (e) {
          e.preventDefault(); return false;
        });
      }
    });
  });


  $('#BucketsDropDown').change(function () {
    
    var bucket = $('#BucketsDropDown option:selected').text();
    if (bucket == "Select a Bucket Type")
    {
      return false;
    }
    console.log("Bucket: " + bucket);

    $.ajax({
      type: 'POST',
      url: $('#getDropdownActionUrl').data('url'),
      data: { typeofData: "StationTypesList", filter: customer, bucketfilter: bucket },
      success: function (data) {
        var result = '';

        var items = '<option>Select a Station Type</option>';

        $.each(data, function (i, stationtype) {
          items += "<option value='" + stationtype.id + "'>" + stationtype.StationTypeName + "</option>";

          result += '<li><div id="selectedStationTypeName" style="border-radius:15px;">' + stationtype.StationTypeName + '</div><div id="selectedStationTypeID" style="display:none;">' + stationtype.id + '</div></li>';
        });

        $('#bucketedstationtypesresultset').html(result);

        /* Get all rows from your 'table' but not the first one 
        * that includes headers. */
        var row = '';
        var selectedFFInstanceID = '';
        var rows = $('#bucketedstationtypesresultset li');

        $(document).bind('selectstart dragstart', function (e) {
          e.preventDefault(); return false;
        });
      }
    });
  });

  $('#Finish').click(function () {
  
    if ($('#CustomersDropDown option:selected').text() == "Select a Customer Project") {
      alert("Please select available station types")
      return false;
    }
    
    if ($('#BucketsDropDown option:selected').text() == "Select a Bucket Type")
    {
      alert("Please select at least one bucket")
      return false;
    }

    var orderedstationtypes = $('#bucketedstationtypesresultset li');

    jsonOrderedStationTypes = [];

    $.each(orderedstationtypes, function (i, stationtype) {
      var row = $(stationtype).find("#selectedStationTypeName").text();
      jsonOrderedStationTypes.push(row);
      console.log(row);

    });

    var nonbucketedstationtypes = $('#stationtypesresultset li');

    jsonNonBucketedStationTypes = [];

    $.each(nonbucketedstationtypes, function (i, stationtype) {
      var row = $(stationtype).find("#selectedStationTypeName").text();
      jsonNonBucketedStationTypes.push(row);
      console.log(row);

    });


    var bucketstntypesmodel = {
      bucketname: $('#BucketsDropDown option:selected').text(),
      orderedstationtypes: jsonOrderedStationTypes,
      nonbucketedstationtypes: jsonNonBucketedStationTypes
    }
    $.ajax({
      type: 'POST',
      url: $('#DimStationTypesUpdateUrl').data('url'),
      data: JSON.stringify(bucketstntypesmodel),
      contentType: 'application/json',
      dataType: 'json',
      sucess: function (result) {
        alert("Bucket assignment is successful")
        $('#bigblue').hide();

      }
    });
  });
});

$("#stationtypesresultset, #bucketedstationtypesresultset").sortable({
  connectWith: ".simple_with_animation"
}).disableSelection();


$("#availablecustomersresultset, #assignedcustomersresultset").sortable({
  connectWith: ".simple_with_animation"
});