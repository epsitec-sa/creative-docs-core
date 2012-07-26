Ext.define('Epsitec.cresus.webcore.CollectionSummaryTile', {
  extend: 'Epsitec.cresus.webcore.SummaryTile',
  alternateClassName: ['Epsitec.CollectionSummaryTile'],
  alias: 'widget.collectionsummarytile',

  /* Properties */

  propertyAccessorId: null,
  entityType: null,
  hideRemoveButton: false,
  hideAddButton: false,

  /* Constructor */

  constructor: function(o) {
    var options = o || {};
    this.addPlusMinusButtons(options);

    this.callParent([options]);
    return this;
  },

  /* Additional methods */

  addPlusMinusButtons: function(options) {
    options.tools = options.tools || [];

    if (!options.hideAddButton) {
      options.tools.push({
        type: 'plus',
        tooltip: 'Add a new item',
        handler: this.addEntity,
        scope: this
      });
    }

    if (!options.hideRemoveButton) {
      options.tools.push({
        type: 'minus',
        tooltip: 'Remove this item',
        handler: this.deleteEntity,
        scope: this
      });
    }
  },

  addEntity: function() {
    this.setLoading();
    Ext.Ajax.request({
      url: 'proxy/collection/create',
      method: 'POST',
      params: {
        parentEntityId: this.column.entityId,
        entityType: this.entityType,
        propertyAccessorId: this.propertyAccessorId
      },
      callback: this.addEntityCallback,
      scope: this
    });
  },

  addEntityCallback: function(options, success, response) {
    var json, entityId;

    this.setLoading(false);

    if (!success) {
      Epsitec.ErrorHandler.handleError(response);
      return;
    }

    json = Epsitec.Tools.decodeJson(response.responseText);
    if (json === null) {
      return;
    }

    entityId = json.content;
    this.column.addEntityColumn(
        this.subViewMode, this.subViewId, entityId, true
    );
  },

  deleteEntity: function() {
    this.setLoading();
    Ext.Ajax.request({
      url: 'proxy/collection/delete',
      method: 'POST',
      params: {
        parentEntityId: this.column.entityId,
        deletedEntityId: this.entityId,
        propertyAccessorId: this.propertyAccessorId
      },
      callback: this.deleteEntityCallback,
      scope: this
    });
  },

  deleteEntityCallback: function(options, sucess, response) {
    this.setLoading(false);

    if (!sucess) {
      Epsitec.ErrorHandler.handleError(response);
      return;
    }

    if (this.isSelected()) {
      this.column.removeToRight();
    }
    this.column.refreshToLeft(true);
  }
});
