function NotificationsToastr() {
  this.hub = $.connection.notificationHub;
  var notificationsClient = null;

  //Initialize
  this.init = function(username,client) {
    this.hub.state.connectionId = this.hub.connection.id;
    this.hub.state.userName = username;
    notificationsClient = client;
  };

  //Entry points for calling hub

  this.WarningToastTo = function(connectionId, title, message, datasetId, entityId) {
    this.hub.server.warningToast(connectionId, title, message, datasetId, entityId);

  };

  this.hub.client.StickyWarningNavToast = function(title, msg, header, field, error, datasetId, entityId) {

    var notif = notificationsClient;

    var path = {};
    path.id = entityId;
    path.name = datasetId;

    var message = {
        title: title,
        body: msg
    };
   
    var errorField = {
        name: field,
        message: error,
        header: header
    };

    toastr.options = {
      'debug': false,
      'positionClass': 'toast-top-full-width',
      'onclick': function() {
          Epsitec.Cresus.Core.app.showEditableEntity(path, message, errorField, notif.displayErrorInTile);
      },
      'fadeIn': 300,
      'fadeOut': 1000,
      'timeOut': 0
    };
    toastr.warning(message.body, message.title);
  };

  this.hub.client.Toast = function(title, msg, datasetId, entityId) {
    var path = {};
    path.id = entityId;
    path.name = datasetId;

    var message = {
        title: title,
        body: msg,
    };

    toastr.options = {
      'debug': false,
      'positionClass': 'toast-top-right',
      'fadeOut': 1000,
      'timeOut': 5000,
      'extendedTimeOut': 5000
    };
    toastr.info(message.body, message.title);
  };
}
