Ext.define('Epsitec.cresus.webcore.Enumeration', {
  extend: 'Ext.Base',
  alternateClassName: ['Epsitec.Enumeration'],

  statics: {
    getStore: function(name) {
      var store = Ext.data.StoreManager.lookup(name);

      if (Epsitec.Tools.isUndefined(store) || store === null) {
        store = Ext.create('Ext.data.Store', {
          fields: ['id', 'name'],
          autoLoad: true,
          proxy: Ext.create('Ext.data.proxy.Ajax', {
            type: 'ajax',
            url: 'proxy/enum/get/' + name,
            reader: {
              type: 'json',
              root: 'content.values'
            }
          })
        });
      }

      return store;
    }
  }
});
