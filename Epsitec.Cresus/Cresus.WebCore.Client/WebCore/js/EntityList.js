Ext.define('Epsitec.cresus.webcore.EntityList', {
  extend: 'Ext.grid.Panel',
  alternateClassName: ['Epsitec.EntityList'],

  /* Config */

  border: false,
  viewConfig: {
    emptyText: 'Nothing to display'
  },

  /* Properties */

  databaseName: null,
  onSelectionChangeCallback: null,

  /* Constructor */

  constructor: function(options) {
    var newOptions = {
      dockedItems: [
        this.createToolbar(options)
      ],
      store: this.createStore(options),
      selModel: this.createSelModel(options),
      onSelectionChangeCallback: options.onSelectionChange,
      listeners: {
        selectionchange: this.onSelectionChangeHandler,
        scope: this
      }
    };
    Ext.applyIf(newOptions, options);

    this.callParent([newOptions]);
    return this;
  },

  /* Additional methods */

  createSelModel: function(options) {
    if (options.multiSelect) {
      return {
        type: 'rowmodel',
        mode: 'MULTI'
      };
    }
    else {
      return {
        selType: 'rowmodel',
        allowDeselect: true,
        mode: 'SINGLE'
      };
    }
  },

  createStore: function(options) {
    return Ext.create('Ext.data.Store', {
      fields: options.fields,
      sorters: options.sorters,
      autoLoad: true,
      pageSize: 100,
      remoteSort: true,
      buffered: true,
      proxy: {
        type: 'ajax',
        url: 'proxy/database/get/' + options.databaseName,
        reader: {
          type: 'json',
          root: 'content.entities',
          totalProperty: 'content.total'
        },
        encodeSorters: this.encodeSorters
      }
    });
  },

  encodeSorters: function(sorters) {
    var sorterStrings = sorters.map(function(s) {
      return s.property + ':' + s.direction;
    });

    return sorterStrings.join(';');
  },

  createToolbar: function(options) {
    return Ext.create('Ext.Toolbar', {
      dock: 'top',
      items: this.createButtons(options)
    });
  },

  createButtons: function(options) {
    var buttons = options.toolbarButtons || [];

    buttons.push(Ext.create('Ext.Button', {
      text: 'Refresh',
      iconCls: 'icon-refresh',
      listeners: {
        click: this.onRefreshHandler,
        scope: this
      }
    }));

    return buttons;
  },

  onRefreshHandler: function() {
    this.reloadStore();
  },

  reloadStore: function() {

    // A call to this.store.reload() should work here, but it has a bug. It is
    // complicated, but if there are not enough rows of data, and a new row is
    // added, this row is not displayed, even with several calls to this
    // method.
    //this.store.reload();

    // A call to this.store.load() should work here, but it has two bugs. It is
    // also complicated, but if there are enough rows of data, and a new row is
    // added, the scroll bar will bump when it reaches the bottom and we can
    // never see the last row. And if we delete an row and click where it was,
    // it is the deleted element that is selected internally, instead of being
    // the one that is displayed. A call to this.store.removeAll() corrects
    // those 2 bugs. But there is a third one. A call to this.store.load()
    // resets the position of the scroll bar to the top, whereas a call to
    // this.store.reload() would keep it. I did not find any workaround for this
    // yet.
    this.store.removeAll();
    this.store.load();
  },

  onSelectionChangeHandler: function(view, selection, options) {
    var entityItems = this.getItems(selection);

    if (this.onSelectionChangeCallback !== null) {
      this.onSelectionChangeCallback.execute([entityItems]);
    }
  },

  getSelectedItems: function() {
    var selection = this.getSelectionModel().getSelection();
    return this.getItems(selection);
  },

  getItems: function(selection) {
    return selection.map(this.getItem);
  },

  getItem: function(row) {
    return {
      id: row.get('id'),
      summary: row.get('summary')
    };
  }
});
