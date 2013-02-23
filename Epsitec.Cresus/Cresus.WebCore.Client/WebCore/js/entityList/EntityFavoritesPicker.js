Ext.require([
  'Epsitec.cresus.webcore.entityList.EntityListPanel',
  'Epsitec.cresus.webcore.tools.EntityPicker'
],
function () {
  Ext.define('Epsitec.cresus.webcore.entityList.EntityFavoritesPicker', {
    extend: 'Epsitec.cresus.webcore.tools.EntityPicker',
    alternateClassName: ['Epsitec.EntityFavoritesPicker'],

    /* Properties */

    entityListPanel1: null,
    entityListPanel2: null,
    tabPanel: null,
    activeEntityListPanel: null,

    /* Constructor */

    constructor: function (options) {
      var newOptions,
          list1, list2;

      list1 = options.list;
      list2 = {};
      Ext.apply(list2, list1);

      delete list2.favoritesId;

      this.entityListPanel1 = this.createEntityListPanel(list1);
      this.entityListPanel2 = this.createEntityListPanel(list2);

      this.tabPanel = new Ext.TabPanel({
        xtype: 'tabpanel',
        id: 'tabpanel',
        activeTab: 0,
        layoutOnTabChange: true,
        items: [{
          xtype: 'panel',
          layout: 'fit',
          title: 'Favoris',
          items: [this.entityListPanel1],
          entityListPanel: this.entityListPanel1
        }, {
          xtype: 'panel',
          layout: 'fit',
          title: 'Liste compl\u00E8te',
          items: [this.entityListPanel2],
          entityListPanel: this.entityListPanel2
        }],
        listeners: {
          tabchange: this.handleTabChange,
          scope: this
        }
      });

      this.activeEntityListPanel = this.entityListPanel1;

      newOptions = {
        items: [this.tabPanel]
      };
      Ext.applyIf(newOptions, options);

      this.callParent([newOptions]);
      return this;
    },

    /* Additional methods */

    handleTabChange: function (tabPanel, newCard, oldCard, eOpts) {
      this.activeEntityListPanel = newCard.entityListPanel;
    },

    createEntityListPanel: function (options) {
      return Ext.create('Epsitec.EntityListPanel', {
        container: {},
        list: options
      });
    },

    getSelectedItems: function () {
      return this.activeEntityListPanel.getEntityList().getSelectedItems();
    },

    statics: {
      showDatabase: function (databaseName, favoritesId, multiSelect, callback) {
        this.show(callback, {
          entityListTypeName: 'Epsitec.DatabaseEntityList',
          databaseName: databaseName,
          favoritesId: favoritesId,
          multiSelect: multiSelect,
          onSelectionChange: null
        });
      },

      showSet: function (viewId, entityId, databaseDefinition, callback) {
        this.show(callback, {
          entityListTypeName: 'Epsitec.SetEntityList',
          viewId: viewId,
          entityId: entityId,
          columnDefinitions: databaseDefinition.columns,
          sorterDefinitions: databaseDefinition.sorters,
          multiSelect: true,
          onSelectionChange: null
        });
      },

      show: function (callback, listOptions) {
        var entityListPicker = Ext.create('Epsitec.EntityFavoritesPicker', {
          list: listOptions,
          callback: callback
        });
        entityListPicker.show();
      }
    }
  });
});
