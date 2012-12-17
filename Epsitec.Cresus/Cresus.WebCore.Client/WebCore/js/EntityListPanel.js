Ext.require([
  'Epsitec.cresus.webcore.DatabaseEntityList',
  'Epsitec.cresus.webcore.DatabaseEditableEntityList',
  'Epsitec.cresus.webcore.SetEntityList',
  'Epsitec.cresus.webcore.SetEditableEntityList',
  'Epsitec.cresus.webcore.Tools'
],
function() {
  Ext.define('Epsitec.cresus.webcore.EntityListPanel', {
    extend: 'Ext.panel.Panel',
    alternateClassName: ['Epsitec.EntityListPanel'],

    /* Config */

    layout: 'fit',

    /* Properties */

    entityList: null,

    /* Constructor */

    constructor: function(options) {
      this.callParent([options.container]);
      this.setupEntityList(options.list);
      return this;
    },

    /* Additional methods */

    setupEntityList: function(options) {
      if (Ext.isDefined(options.databaseName)) {
        this.setupDatabaseEntityList(options);
      }
      else {
        this.setupSetEntityList(options);
      }
    },

    setupDatabaseEntityList: function(options) {
      this.setLoading(true);
      Ext.Ajax.request({
        url: 'proxy/database/definition/' + options.databaseName,
        callback: function(requestOptions, success, response) {
          this.setupDatabaseEntityListCallback(options, success, response);
        },
        scope: this
      });
    },

    setupDatabaseEntityListCallback: function(options, success, response) {
      var json;

      this.setLoading(false);

      json = Epsitec.Tools.processResponse(success, response);
      if (json === null) {
        return;
      }

      this.createEntityList(options.entityListTypeName, {
        databaseName: options.databaseName,
        columnDefinitions: json.content.columns,
        sorterDefinitions: json.content.sorters,
        multiSelect: options.multiSelect,
        onSelectionChange: options.onSelectionChange
      });
    },

    setupSetEntityList: function(options) {
      this.createEntityList(options.entityListTypeName, options);
    },

    createEntityList: function(typeName, options) {
      this.entityList = Ext.create(typeName, options);
      this.add(this.entityList);
    },

    getEntityList: function() {
      return this.entityList;
    }
  });
});
