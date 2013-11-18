function EntityBagHub() {
  this.hub = $.connection.notificationHub;
  var entityBagClient = null;

  //Initialize
  this.init = function(con,username, client) {   
    this.hub = con.notificationHub;
    this.hub.state.connectionId = this.hub.connection.id;
    this.hub.state.userName = username;
    entityBagClient = client;
    this.hub.server.setupUserConnection();
  };

  //Entry points for calling hub
  this.hub.client.AddToBag = function(title, summary, entityId) {
    var app = Epsitec.Cresus.Core.getApplication();
    var entity = {
          summary: summary,
          entityType: title,
          id: entityId
        };

    app.addEntityToBag(entity,null);
  };

  this.hub.client.RemoveFromBag = function(title, summary, entityId) {
    alert('title');
  };
  
}
