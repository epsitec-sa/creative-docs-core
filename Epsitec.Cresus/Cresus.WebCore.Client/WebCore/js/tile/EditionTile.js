// This class represents the tiles that contain a form with edition fields that
// the user can use to update the data of a entity.

Ext.require([
  'Epsitec.cresus.webcore.field.EntityCollectionField',
  'Epsitec.cresus.webcore.field.EntityReferenceField',
  'Epsitec.cresus.webcore.field.EnumerationField',
  'Epsitec.cresus.webcore.tile.EntityTile',
  'Epsitec.cresus.webcore.tools.ErrorHandler',
  'Epsitec.cresus.webcore.tools.Texts',
  'Epsitec.cresus.webcore.tools.Tools'
],
function() {
  Ext.define('Epsitec.cresus.webcore.tile.EditionTile', {
    extend: 'Epsitec.cresus.webcore.tile.EntityTile',
    alternateClassName: ['Epsitec.EditionTile'],
    alias: 'widget.epsitec.editiontile',

    /* Configuration */

    width: 300,
    defaults: {
      anchor: '100%'
    },
    fieldDefaults: {
      labelAlign: 'top',
      msgTarget: 'side'
    },

    /* Properties */

    errorField: null,

    /* Constructor */

    constructor: function(options) {
      var newOptions = {
        url: 'proxy/entity/edit/' + options.entityId,
        fbar: {
          items: this.getButtons(),
          cls: 'tile'
        }
      };
      Ext.applyIf(newOptions, options);

      this.callParent([newOptions]);
      return this;
    },

    getButtons: function() {
      var resetButton, saveButton;

      resetButton = Ext.create('Ext.button.Button', {
        margin: '0 0 0 0',
        text: Epsitec.Texts.getResetLabel(),
        listeners: {
          click: this.onResetClick,
          scope: this
        }
      });

      saveButton = Ext.create('Ext.button.Button', {
        margin: '0 5 0 0',
        text: Epsitec.Texts.getSaveLabel(),
        listeners: {
          click: this.onSaveClick,
          scope: this
        }
      });

      return [saveButton, resetButton];
    },

    onResetClick: function() {
      this.hideError();
      this.getForm().reset();
    },

    onSaveClick: function() {
      this.hideError();
      var form = this.getForm();
      if (form.isValid()) {
        this.setLoading();
        form.submit({
          success: function(form, action) {
            this.onSaveClickCallback(true, form, action);
          },
          failure: function(form, action) {
            this.onSaveClickCallback(false, form, action);
          },
          scope: this
        });
      }
    },

    onSaveClickCallback: function(success, form, action) {
      var responseData, json, businessError;

      this.setLoading(false);

      responseData = Epsitec.Tools.tryDecodeRespone(action.response);
      if (!responseData.success)
      {
        Epsitec.ErrorHandler.handleDefaultFailure();
        return;
      }

      json = responseData.data;

      if (success) {
        this.column.refreshToLeft(true);
      }
      else {
        Epsitec.ErrorHandler.handleFormError(action);

        businessError = json.content.businesserror;
        if (Ext.isDefined(businessError))
        {
          this.showError(businessError);
        }
      }
    },

    showError: function(error) {
      if (this.errorField === null)
      {
        this.errorField = Ext.create('Ext.form.field.Display', {
          baseBodyCls: 'business-error',
          fieldCls: null,
          fieldLabel: Epsitec.Texts.getErrorTitle()
        }),
        this.insert(0, this.errorField);
      }
      this.errorField.setValue(error);
    },

    hideError: function() {
      if (this.errorField !== null)
      {
        this.remove(this.errorField);
        this.errorField = null;
      }
    },

    showFieldError: function(fieldName, message) {
      var field = this.getForm().findField(fieldName);
      if (field) {
        field.markInvalid(message);
        field.focus();
      }
    },

    // Overrides the method defined in Tile.
    getState: function() {
      return {
        type: 'editionTile',
        entityId: this.entityId
      };
    },

    // Overrides the method defined in Tile.
    setState: function(state) {
      // Nothing to do here, as this kind of tile doesn't really have a state
      // to keep track.
    },

    // Overrides the method defined in Tile.
    isStateApplicable: function(state) {
      return state.type === 'editionTile' && state.entityId === this.entityId;
    }
  });
});
