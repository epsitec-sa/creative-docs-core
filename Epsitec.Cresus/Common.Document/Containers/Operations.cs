using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Drawing;

namespace Epsitec.Common.Document.Containers
{
	/// <summary>
	/// La classe Containers.Operations contient toutes les op�rations.
	/// </summary>
	[SuppressBundleSupport]
	public class Operations : Abstract
	{
		public Operations(Document document) : base(document)
		{
			this.tabIndex = 0;

			// D�placement.
			this.CreateHeader(ref this.boxMove, ref this.toolBarMove, Res.Strings.Action.MoveMain);
			this.CreateFieldMove(this.toolBarMove, ref this.fieldMoveH, Res.Strings.Action.MoveValueX);
			this.CreateButton(this.toolBarMove, ref this.buttonMoveHi, "MoveHi", Res.Strings.Action.MoveLeft,  new MessageEventHandler(this.HandleButtonMoveHi));
			this.CreateButton(this.toolBarMove, ref this.buttonMoveH,  "MoveH",  Res.Strings.Action.MoveRight, new MessageEventHandler(this.HandleButtonMoveH));
			this.CreateSeparator(this.toolBarMove);
			this.CreateFieldMove(this.toolBarMove, ref this.fieldMoveV, Res.Strings.Action.MoveValueY);
			this.CreateButton(this.toolBarMove, ref this.buttonMoveVi, "MoveVi", Res.Strings.Action.MoveDown, new MessageEventHandler(this.HandleButtonMoveVi));
			this.CreateButton(this.toolBarMove, ref this.buttonMoveV,  "MoveV",  Res.Strings.Action.MoveUp,   new MessageEventHandler(this.HandleButtonMoveV));

			// Rotation.
			this.CreateHeader(ref this.boxRot, ref this.toolBarRot, Res.Strings.Action.RotateMain);
			this.CreateButton(this.toolBarRot, ref this.buttonRotate90,  "Rotate90",  Res.Strings.Action.Rotate90,  new MessageEventHandler(this.HandleButtonRotate90));
			this.CreateButton(this.toolBarRot, ref this.buttonRotate180, "Rotate180", Res.Strings.Action.Rotate180, new MessageEventHandler(this.HandleButtonRotate180));
			this.CreateButton(this.toolBarRot, ref this.buttonRotate270, "Rotate270", Res.Strings.Action.Rotate270, new MessageEventHandler(this.HandleButtonRotate270));
			this.CreateSeparator(this.toolBarRot);
			this.CreateFieldRot(this.toolBarRot, ref this.fieldRotate, Res.Strings.Action.RotateValue);
			this.CreateButton(this.toolBarRot, ref this.buttonRotate,  "Rotate",  Res.Strings.Action.RotateFreeCCW, new MessageEventHandler(this.HandleButtonRotate));
			this.CreateButton(this.toolBarRot, ref this.buttonRotatei, "Rotatei", Res.Strings.Action.RotateFreeCW,  new MessageEventHandler(this.HandleButtonRotatei));

			// Miroir.
			this.CreateHeader(ref this.boxMirror, ref this.toolBarMirror, Res.Strings.Action.MirrorMain);
			this.CreateButton(this.toolBarMirror, ref this.buttonMirrorH, "MirrorH", Res.Strings.Action.MirrorH, new MessageEventHandler(this.HandleButtonMirrorH));
			this.CreateButton(this.toolBarMirror, ref this.buttonMirrorV, "MirrorV", Res.Strings.Action.MirrorV, new MessageEventHandler(this.HandleButtonMirrorV));

			// Echelle.
			this.CreateHeader(ref this.boxScale, ref this.toolBarScale, Res.Strings.Action.ScaleMain);
			this.CreateButton(this.toolBarScale, ref this.buttonScaleDiv2, "ScaleDiv2", Res.Strings.Action.ScaleDiv2, new MessageEventHandler(this.HandleButtonScaleDiv2));
			this.CreateButton(this.toolBarScale, ref this.buttonScaleMul2, "ScaleMul2", Res.Strings.Action.ScaleMul2, new MessageEventHandler(this.HandleButtonScaleMul2));
			this.CreateSeparator(this.toolBarScale);
			this.CreateFieldScale(this.toolBarScale, ref this.fieldScale, Res.Strings.Action.ScaleValue);
			this.CreateButton(this.toolBarScale, ref this.buttonScaleDivFree, "ScaleDivFree", Res.Strings.Action.ScaleDivFree, new MessageEventHandler(this.HandleButtonScaleDivFree));
			this.CreateButton(this.toolBarScale, ref this.buttonScaleMulFree, "ScaleMulFree", Res.Strings.Action.ScaleMulFree, new MessageEventHandler(this.HandleButtonScaleMulFree));

			// Alignement.
			this.CreateHeader(ref this.boxAlign, ref this.toolBarAlign, Res.Strings.Action.Align);
			this.CreateButton(this.toolBarAlign, ref this.buttonAlignGrid,    "AlignGrid",    Res.Strings.Action.AlignGrid,    new MessageEventHandler(this.HandleButtonAlignGrid));
			this.CreateSeparator(this.toolBarAlign);
			this.CreateButton(this.toolBarAlign, ref this.buttonAlignLeft,    "AlignLeft",    Res.Strings.Action.AlignLeft,    new MessageEventHandler(this.HandleButtonAlignLeft));
			this.CreateButton(this.toolBarAlign, ref this.buttonAlignCenterX, "AlignCenterX", Res.Strings.Action.AlignCenterX, new MessageEventHandler(this.HandleButtonAlignCenterX));
			this.CreateButton(this.toolBarAlign, ref this.buttonAlignRight,   "AlignRight",   Res.Strings.Action.AlignRight,   new MessageEventHandler(this.HandleButtonAlignRight));
			this.CreateSeparator(this.toolBarAlign);
			this.CreateButton(this.toolBarAlign, ref this.buttonAlignTop,     "AlignTop",     Res.Strings.Action.AlignTop,     new MessageEventHandler(this.HandleButtonAlignTop));
			this.CreateButton(this.toolBarAlign, ref this.buttonAlignCenterY, "AlignCenterY", Res.Strings.Action.AlignCenterY, new MessageEventHandler(this.HandleButtonAlignCenterY));
			this.CreateButton(this.toolBarAlign, ref this.buttonAlignBottom,  "AlignBottom",  Res.Strings.Action.AlignBottom,  new MessageEventHandler(this.HandleButtonAlignBottom));

			// Distribution.
			this.CreateHeader(ref this.boxShare, ref this.toolBarShare, Res.Strings.Action.Share);
			this.CreateButton(this.toolBarShare, ref this.buttonShareSpaceX,  "ShareSpaceX",  Res.Strings.Action.ShareSpaceX,  new MessageEventHandler(this.HandleButtonShareSpaceX));
			this.CreateButton(this.toolBarShare, ref this.buttonShareLeft,    "ShareLeft",    Res.Strings.Action.ShareLeft,    new MessageEventHandler(this.HandleButtonShareLeft));
			this.CreateButton(this.toolBarShare, ref this.buttonShareCenterX, "ShareCenterX", Res.Strings.Action.ShareCenterX, new MessageEventHandler(this.HandleButtonShareCenterX));
			this.CreateButton(this.toolBarShare, ref this.buttonShareRight,   "ShareRight",   Res.Strings.Action.ShareRight,   new MessageEventHandler(this.HandleButtonShareRight));
			this.CreateSeparator(this.toolBarShare);
			this.CreateButton(this.toolBarShare, ref this.buttonShareSpaceY,  "ShareSpaceY",  Res.Strings.Action.ShareSpaceY,  new MessageEventHandler(this.HandleButtonShareSpaceY));
			this.CreateButton(this.toolBarShare, ref this.buttonShareTop,     "ShareTop",     Res.Strings.Action.ShareTop,     new MessageEventHandler(this.HandleButtonShareTop));
			this.CreateButton(this.toolBarShare, ref this.buttonShareCenterY, "ShareCenterY", Res.Strings.Action.ShareCenterY, new MessageEventHandler(this.HandleButtonShareCenterY));
			this.CreateButton(this.toolBarShare, ref this.buttonShareBottom,  "ShareBottom",  Res.Strings.Action.ShareBottom,  new MessageEventHandler(this.HandleButtonShareBottom));

			// Ajustements.
			this.CreateHeader(ref this.boxAdjust, ref this.toolBarAdjust, Res.Strings.Action.Adjust);
			this.CreateButton(this.toolBarAdjust, ref this.buttonAdjustWidth,  "AdjustWidth",  Res.Strings.Action.AdjustWidth,  new MessageEventHandler(this.HandleButtonAdjustWidth));
			this.CreateButton(this.toolBarAdjust, ref this.buttonAdjustHeight, "AdjustHeight", Res.Strings.Action.AdjustHeight, new MessageEventHandler(this.HandleButtonAdjustHeight));

			// Couleurs.
			this.CreateHeader(ref this.boxColor, ref this.toolBarColor, Res.Strings.Action.Color);
			this.CreateButton(this.toolBarColor, ref this.buttonColorToRGB,  "ColorToRGB",  Res.Strings.Action.ColorToRGB,  new MessageEventHandler(this.HandleButtonColorToRGB));
			this.CreateButton(this.toolBarColor, ref this.buttonColorToCMYK, "ColorToCMYK", Res.Strings.Action.ColorToCMYK, new MessageEventHandler(this.HandleButtonColorToCMYK));
			this.CreateButton(this.toolBarColor, ref this.buttonColorToGray, "ColorToGray", Res.Strings.Action.ColorToGray, new MessageEventHandler(this.HandleButtonColorToGray));
			this.CreateSeparator(this.toolBarColor);
			this.CreateFieldColor(this.toolBarColor, ref this.fieldColor, Res.Strings.Action.ColorValue);
			this.CreateButton(this.toolBarColor, ref this.buttonColorStrokeDark,  "ColorStrokeDark",  Res.Strings.Action.ColorStrokeDark,  new MessageEventHandler(this.HandleButtonColorStrokeDark));
			this.CreateButton(this.toolBarColor, ref this.buttonColorStrokeLight, "ColorStrokeLight", Res.Strings.Action.ColorStrokeLight, new MessageEventHandler(this.HandleButtonColorStrokeLight));
			this.CreateButton(this.toolBarColor, ref this.buttonColorFillDark,    "ColorFillDark",    Res.Strings.Action.ColorFillDark,    new MessageEventHandler(this.HandleButtonColorFillDark));
			this.CreateButton(this.toolBarColor, ref this.buttonColorFillLight,   "ColorFillLight",   Res.Strings.Action.ColorFillLight,   new MessageEventHandler(this.HandleButtonColorFillLight));
		}

		// Cr�e l'en-t�te d'un groupe.
		protected void CreateHeader(ref GroupBox group, ref HToolBar bar, string title)
		{
			group = new GroupBox(this);
			group.Height = 45;
			group.Text = title;
			group.Dock = DockStyle.Top;
			group.DockMargins = new Margins(0, 0, 0, 10);
			group.TabNavigation = Widget.TabNavigationMode.ForwardTabPassive;

			bar = new HToolBar(group);
			bar.Dock = DockStyle.Top;
			bar.DockMargins = new Margins(0, 0, 0, 10);
			bar.TabIndex = this.tabIndex++;
			bar.TabNavigation = Widget.TabNavigationMode.ForwardTabActive;
		}

		// Cr�e un IconButton. 
		protected void CreateButton(HToolBar bar, ref IconButton button, string icon, string tooltip, MessageEventHandler handler)
		{
			button = new IconButton(Misc.Icon(icon));
			button.Clicked += handler;
			button.TabIndex = this.tabIndex++;
			button.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;
			ToolTip.Default.SetToolTip(button, tooltip);
			bar.Items.Add(button);
		}

		// Cr�e un champ �ditable pour un d�placement.
		protected void CreateFieldMove(HToolBar bar, ref TextFieldReal field, string tooltip)
		{
			field = new TextFieldReal();
			this.document.Modifier.AdaptTextFieldRealDimension(field);
			field.Width = 50;
			if ( this.document.Type == DocumentType.Pictogram )
			{
				field.InternalValue = 1.0M;
			}
			else
			{
				field.InternalValue = 100.0M;  // 10mm
			}
			field.TabIndex = this.tabIndex++;
			field.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;
			ToolTip.Default.SetToolTip(field, tooltip);
			bar.Items.Add(field);
		}

		// Cr�e un champ �ditable pour une rotation.
		protected void CreateFieldRot(HToolBar bar, ref TextFieldReal field, string tooltip)
		{
			field = new TextFieldReal();
			this.document.Modifier.AdaptTextFieldRealAngle(field);
			field.Width = 50;
			field.InternalValue = 10.0M;
			field.TabIndex = tabIndex++;
			field.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;
			ToolTip.Default.SetToolTip(field, tooltip);
			bar.Items.Add(field);
		}

		// Cr�e un champ �ditable pour une �chelle.
		protected void CreateFieldScale(HToolBar bar, ref TextFieldReal field, string tooltip)
		{
			field = new TextFieldReal();
			this.document.Modifier.AdaptTextFieldRealScalar(field);
			field.Width = 50;
			field.InternalMinValue = 1.0M;
			field.InternalMaxValue = 2.0M;
			field.DefaultValue = 1.0M;
			field.Step = 0.1M;
			field.Resolution = 0.01M;
			field.InternalValue = 1.2M;
			field.TabIndex = tabIndex++;
			field.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;
			ToolTip.Default.SetToolTip(field, tooltip);
			bar.Items.Add(field);
		}

		// Cr�e un champ �ditable pour une couleur.
		protected void CreateFieldColor(HToolBar bar, ref TextFieldReal field, string tooltip)
		{
			field = new TextFieldReal();
			this.document.Modifier.AdaptTextFieldRealPercent(field);
			field.Width = 50;
			field.InternalMinValue = 0.0M;
			field.InternalMaxValue = 1.0M;
			field.DefaultValue = 0.1M;
			field.InternalValue = 0.1M;
			field.TabIndex = tabIndex++;
			field.TabNavigation = Widget.TabNavigationMode.ActivateOnTab;
			ToolTip.Default.SetToolTip(field, tooltip);
			bar.Items.Add(field);
		}

		// Cr�e un s�parateur.
		protected void CreateSeparator(HToolBar bar)
		{
			bar.Items.Add(new IconSeparator());
		}
		

		// Effectue la mise � jour du contenu.
		protected override void DoUpdateContent()
		{
			bool enabled  = (this.document.Modifier.TotalSelected > 0);
			bool enabled2 = (this.document.Modifier.TotalSelected > 1);
			bool enabled3 = (this.document.Modifier.TotalSelected > 2);

			if ( this.document.Modifier.Tool == "Edit" )
			{
				enabled  = false;
				enabled2 = false;
				enabled3 = false;
			}

			this.buttonMoveH.SetEnabled(enabled);
			this.buttonMoveHi.SetEnabled(enabled);
			this.buttonMoveV.SetEnabled(enabled);
			this.buttonMoveVi.SetEnabled(enabled);

			this.buttonRotate90.SetEnabled(enabled);
			this.buttonRotate180.SetEnabled(enabled);
			this.buttonRotate270.SetEnabled(enabled);
			this.buttonRotate.SetEnabled(enabled);
			this.buttonRotatei.SetEnabled(enabled);
			
			this.buttonMirrorH.SetEnabled(enabled);
			this.buttonMirrorV.SetEnabled(enabled);
			
			this.buttonScaleMul2.SetEnabled(enabled);
			this.buttonScaleDiv2.SetEnabled(enabled);
			this.buttonScaleMulFree.SetEnabled(enabled);
			this.buttonScaleDivFree.SetEnabled(enabled);

			this.buttonAlignGrid.SetEnabled(enabled);
			this.buttonAlignLeft.SetEnabled(enabled2);
			this.buttonAlignCenterX.SetEnabled(enabled2);
			this.buttonAlignRight.SetEnabled(enabled2);
			this.buttonAlignTop.SetEnabled(enabled2);
			this.buttonAlignCenterY.SetEnabled(enabled2);
			this.buttonAlignBottom.SetEnabled(enabled2);

			this.buttonShareLeft.SetEnabled(enabled3);
			this.buttonShareCenterX.SetEnabled(enabled3);
			this.buttonShareSpaceX.SetEnabled(enabled3);
			this.buttonShareRight.SetEnabled(enabled3);
			this.buttonShareTop.SetEnabled(enabled3);
			this.buttonShareCenterY.SetEnabled(enabled3);
			this.buttonShareSpaceY.SetEnabled(enabled3);
			this.buttonShareBottom.SetEnabled(enabled3);

			this.buttonAdjustWidth.SetEnabled(enabled2);
			this.buttonAdjustHeight.SetEnabled(enabled2);

			this.buttonColorToRGB.SetEnabled(enabled);
			this.buttonColorToCMYK.SetEnabled(enabled);
			this.buttonColorToGray.SetEnabled(enabled);
			this.buttonColorStrokeDark.SetEnabled(enabled);
			this.buttonColorStrokeLight.SetEnabled(enabled);
			this.buttonColorFillDark.SetEnabled(enabled);
			this.buttonColorFillLight.SetEnabled(enabled);
		}


		private void HandleButtonMoveH(object sender, MessageEventArgs e)
		{
			double dx = (double) this.fieldMoveH.InternalValue;
			this.document.Modifier.MoveSelection(new Point(dx,0));
		}

		private void HandleButtonMoveHi(object sender, MessageEventArgs e)
		{
			double dx = (double) this.fieldMoveH.InternalValue;
			this.document.Modifier.MoveSelection(new Point(-dx,0));
		}

		private void HandleButtonMoveV(object sender, MessageEventArgs e)
		{
			double dy = (double) this.fieldMoveV.InternalValue;
			this.document.Modifier.MoveSelection(new Point(0,dy));
		}

		private void HandleButtonMoveVi(object sender, MessageEventArgs e)
		{
			double dy = (double) this.fieldMoveV.InternalValue;
			this.document.Modifier.MoveSelection(new Point(0,-dy));
		}

		private void HandleButtonRotate90(object sender, MessageEventArgs e)
		{
			this.document.Modifier.RotateSelection(90.0);
		}

		private void HandleButtonRotate180(object sender, MessageEventArgs e)
		{
			this.document.Modifier.RotateSelection(180.0);
		}

		private void HandleButtonRotate270(object sender, MessageEventArgs e)
		{
			this.document.Modifier.RotateSelection(270.0);
		}

		private void HandleButtonRotate(object sender, MessageEventArgs e)
		{
			double angle = (double) this.fieldRotate.InternalValue;
			this.document.Modifier.RotateSelection(angle);
		}

		private void HandleButtonRotatei(object sender, MessageEventArgs e)
		{
			double angle = (double) this.fieldRotate.InternalValue;
			this.document.Modifier.RotateSelection(-angle);
		}

		private void HandleButtonMirrorH(object sender, MessageEventArgs e)
		{
			this.document.Modifier.MirrorSelection(true);
		}

		private void HandleButtonMirrorV(object sender, MessageEventArgs e)
		{
			this.document.Modifier.MirrorSelection(false);
		}

		private void HandleButtonScaleMul2(object sender, MessageEventArgs e)
		{
			this.document.Modifier.ScaleSelection(2.0);
		}

		private void HandleButtonScaleDiv2(object sender, MessageEventArgs e)
		{
			this.document.Modifier.ScaleSelection(0.5);
		}

		private void HandleButtonScaleMulFree(object sender, MessageEventArgs e)
		{
			double scale = (double) this.fieldScale.InternalValue;
			this.document.Modifier.ScaleSelection(scale);
		}

		private void HandleButtonScaleDivFree(object sender, MessageEventArgs e)
		{
			double scale = (double) this.fieldScale.InternalValue;
			this.document.Modifier.ScaleSelection(1.0/scale);
		}

		private void HandleButtonAlignGrid(object sender, MessageEventArgs e)
		{
			this.document.Modifier.AlignGridSelection();
		}

		private void HandleButtonAlignLeft(object sender, MessageEventArgs e)
		{
			this.document.Modifier.AlignSelection(-1, true);
		}

		private void HandleButtonAlignCenterX(object sender, MessageEventArgs e)
		{
			this.document.Modifier.AlignSelection(0, true);
		}

		private void HandleButtonAlignRight(object sender, MessageEventArgs e)
		{
			this.document.Modifier.AlignSelection(1, true);
		}

		private void HandleButtonAlignTop(object sender, MessageEventArgs e)
		{
			this.document.Modifier.AlignSelection(1, false);
		}

		private void HandleButtonAlignCenterY(object sender, MessageEventArgs e)
		{
			this.document.Modifier.AlignSelection(0, false);
		}

		private void HandleButtonAlignBottom(object sender, MessageEventArgs e)
		{
			this.document.Modifier.AlignSelection(-1, false);
		}

		private void HandleButtonShareLeft(object sender, MessageEventArgs e)
		{
			this.document.Modifier.ShareSelection(-1, true);
		}

		private void HandleButtonShareCenterX(object sender, MessageEventArgs e)
		{
			this.document.Modifier.ShareSelection(0, true);
		}

		private void HandleButtonShareSpaceX(object sender, MessageEventArgs e)
		{
			this.document.Modifier.SpaceSelection(true);
		}

		private void HandleButtonShareRight(object sender, MessageEventArgs e)
		{
			this.document.Modifier.ShareSelection(1, true);
		}

		private void HandleButtonShareTop(object sender, MessageEventArgs e)
		{
			this.document.Modifier.ShareSelection(1, false);
		}

		private void HandleButtonShareCenterY(object sender, MessageEventArgs e)
		{
			this.document.Modifier.ShareSelection(0, false);
		}

		private void HandleButtonShareSpaceY(object sender, MessageEventArgs e)
		{
			this.document.Modifier.SpaceSelection(false);
		}

		private void HandleButtonShareBottom(object sender, MessageEventArgs e)
		{
			this.document.Modifier.ShareSelection(-1, false);
		}

		private void HandleButtonAdjustWidth(object sender, MessageEventArgs e)
		{
			this.document.Modifier.AdjustSelection(true);
		}

		private void HandleButtonAdjustHeight(object sender, MessageEventArgs e)
		{
			this.document.Modifier.AdjustSelection(false);
		}

		private void HandleButtonColorToRGB(object sender, MessageEventArgs e)
		{
			this.document.Modifier.ColorSelection(ColorSpace.RGB);
		}

		private void HandleButtonColorToCMYK(object sender, MessageEventArgs e)
		{
			this.document.Modifier.ColorSelection(ColorSpace.CMYK);
		}

		private void HandleButtonColorToGray(object sender, MessageEventArgs e)
		{
			this.document.Modifier.ColorSelection(ColorSpace.Gray);
		}

		private void HandleButtonColorStrokeDark(object sender, MessageEventArgs e)
		{
			double adjust = (double) this.fieldColor.InternalValue;
			this.document.Modifier.ColorSelection(-adjust, true);
		}

		private void HandleButtonColorStrokeLight(object sender, MessageEventArgs e)
		{
			double adjust = (double) this.fieldColor.InternalValue;
			this.document.Modifier.ColorSelection(adjust, true);
		}

		private void HandleButtonColorFillDark(object sender, MessageEventArgs e)
		{
			double adjust = (double) this.fieldColor.InternalValue;
			this.document.Modifier.ColorSelection(-adjust, false);
		}

		private void HandleButtonColorFillLight(object sender, MessageEventArgs e)
		{
			double adjust = (double) this.fieldColor.InternalValue;
			this.document.Modifier.ColorSelection(adjust, false);
		}


		protected GroupBox				boxMove;
		protected HToolBar				toolBarMove;
		protected TextFieldReal			fieldMoveH;
		protected IconButton			buttonMoveH;
		protected IconButton			buttonMoveHi;
		protected TextFieldReal			fieldMoveV;
		protected IconButton			buttonMoveV;
		protected IconButton			buttonMoveVi;

		protected GroupBox				boxRot;
		protected HToolBar				toolBarRot;
		protected IconButton			buttonRotate90;
		protected IconButton			buttonRotate180;
		protected IconButton			buttonRotate270;
		protected TextFieldReal			fieldRotate;
		protected IconButton			buttonRotate;
		protected IconButton			buttonRotatei;
		
		protected GroupBox				boxMirror;
		protected HToolBar				toolBarMirror;
		protected IconButton			buttonMirrorH;
		protected IconButton			buttonMirrorV;
		
		protected GroupBox				boxScale;
		protected HToolBar				toolBarScale;
		protected IconButton			buttonScaleDiv2;
		protected IconButton			buttonScaleMul2;
		protected TextFieldReal			fieldScale;
		protected IconButton			buttonScaleDivFree;
		protected IconButton			buttonScaleMulFree;
		
		protected GroupBox				boxAlign;
		protected HToolBar				toolBarAlign;
		protected IconButton			buttonAlignGrid;
		protected IconButton			buttonAlignLeft;
		protected IconButton			buttonAlignCenterX;
		protected IconButton			buttonAlignRight;
		protected IconButton			buttonAlignTop;
		protected IconButton			buttonAlignCenterY;
		protected IconButton			buttonAlignBottom;
		
		protected GroupBox				boxShare;
		protected HToolBar				toolBarShare;
		protected IconButton			buttonShareLeft;
		protected IconButton			buttonShareCenterX;
		protected IconButton			buttonShareSpaceX;
		protected IconButton			buttonShareRight;
		protected IconButton			buttonShareTop;
		protected IconButton			buttonShareCenterY;
		protected IconButton			buttonShareSpaceY;
		protected IconButton			buttonShareBottom;
		
		protected GroupBox				boxAdjust;
		protected HToolBar				toolBarAdjust;
		protected IconButton			buttonAdjustWidth;
		protected IconButton			buttonAdjustHeight;
		
		protected GroupBox				boxColor;
		protected HToolBar				toolBarColor;
		protected IconButton			buttonColorToRGB;
		protected IconButton			buttonColorToCMYK;
		protected IconButton			buttonColorToGray;
		protected TextFieldReal			fieldColor;
		protected IconButton			buttonColorStrokeDark;
		protected IconButton			buttonColorStrokeLight;
		protected IconButton			buttonColorFillDark;
		protected IconButton			buttonColorFillLight;

		protected int					tabIndex;
	}
}
