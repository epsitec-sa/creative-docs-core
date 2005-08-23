namespace Epsitec.Common.Widgets
{
	using BundleAttribute = Epsitec.Common.Support.BundleAttribute;
	
	/// <summary>
	/// La classe ColorSelector permet de choisir une couleur.
	/// </summary>
	public class ColorSelector : Widget
	{
		public ColorSelector()
		{
			if ( Support.ObjectBundler.IsBooting )
			{
				//	N'initialise rien, car cela prend passablement de temps... et de toute
				//	mani�re, on n'a pas besoin de toutes ces informations pour pouvoir
				//	utiliser IBundleSupport.
				
				return;
			}
			
			this.nbField = 4+3+4+1;
			this.labels = new StaticText[this.nbField];
			this.fields = new TextFieldSlider[this.nbField];
			for ( int i=0 ; i<this.nbField ; i++ )
			{
				this.labels[i] = new StaticText(this);
				this.fields[i] = new TextFieldSlider(this);

				this.fields[i].TabIndex = i+100;
				this.fields[i].TabNavigation = Widget.TabNavigationMode.ActivateOnTab;
				this.fields[i].Value = 0;
				if ( i < 4 )  // r,g,b,a ?
				{
					this.fields[i].MinValue = 0;
					this.fields[i].MaxValue = 255;
					this.fields[i].Step = 10;
					this.fields[i].TextChanged += new Support.EventHandler(this.HandleTextRGBChanged);
				}
				else if ( i == 4 )  // t ?
				{
					this.fields[i].MinValue = 0;
					this.fields[i].MaxValue = 360;
					this.fields[i].Step = 10;
					this.fields[i].TextChanged += new Support.EventHandler(this.HandleTextHSVChanged);
				}
				else if ( i < 7 )  // s,i ?
				{
					this.fields[i].MinValue = 0;
					this.fields[i].MaxValue = 100;
					this.fields[i].Step = 5;
					this.fields[i].TextChanged += new Support.EventHandler(this.HandleTextHSVChanged);
				}
				else if ( i< 11 )  // c,m,y,k ?
				{
					this.fields[i].MinValue = 0;
					this.fields[i].MaxValue = 100;
					this.fields[i].Step = 5;
					this.fields[i].TextChanged += new Support.EventHandler(this.HandleTextCMYKChanged);
				}
				else	// g ?
				{
					this.fields[i].MinValue = 0;
					this.fields[i].MaxValue = 100;
					this.fields[i].Step = 5;
					this.fields[i].TextChanged += new Support.EventHandler(this.HandleTextGrayChanged);
				}
			}

			int index = 100;
			this.fields[ 0].TabIndex = index++;
			this.fields[ 1].TabIndex = index++;
			this.fields[ 2].TabIndex = index++;
			this.fields[ 4].TabIndex = index++;
			this.fields[ 5].TabIndex = index++;
			this.fields[ 6].TabIndex = index++;
			this.fields[ 7].TabIndex = index++;
			this.fields[ 8].TabIndex = index++;
			this.fields[ 9].TabIndex = index++;
			this.fields[10].TabIndex = index++;
			this.fields[11].TabIndex = index++;
			this.fields[ 3].TabIndex = index++;

			this.fields[0].Color = Drawing.Color.FromRGB(1,0,0);  // r
			this.fields[1].Color = Drawing.Color.FromRGB(0,1,0);  // v
			this.fields[2].Color = Drawing.Color.FromRGB(0,0,1);  // b
			this.fields[3].Color = Drawing.Color.FromRGB(0.5,0.5,0.5);
			ToolTip.Default.SetToolTip(this.fields[0], Res.Strings.ColorSelector.LongRed);
			ToolTip.Default.SetToolTip(this.fields[1], Res.Strings.ColorSelector.LongGreen);
			ToolTip.Default.SetToolTip(this.fields[2], Res.Strings.ColorSelector.LongBlue);
			ToolTip.Default.SetToolTip(this.fields[3], Res.Strings.ColorSelector.LongAlpha);

			this.fields[4].Color = Drawing.Color.FromRGB(0,0,0);
			this.fields[4].BackColor = Drawing.Color.FromRGB(0.5,0.5,0.5);
			ToolTip.Default.SetToolTip(this.fields[4], Res.Strings.ColorSelector.LongHue);

			this.fields[5].Color = Drawing.Color.FromRGB(0,0,0);
			this.fields[5].BackColor = Drawing.Color.FromRGB(1,1,1);
			ToolTip.Default.SetToolTip(this.fields[5], Res.Strings.ColorSelector.LongSaturation);
			
			this.fields[6].Color = Drawing.Color.FromRGB(1,1,1);
			this.fields[6].BackColor = Drawing.Color.FromRGB(0,0,0);
			ToolTip.Default.SetToolTip(this.fields[6], Res.Strings.ColorSelector.LongValue);

			this.fields[ 7].Color = Drawing.Color.FromRGB(0,1,1);  // c
			this.fields[ 8].Color = Drawing.Color.FromRGB(1,0,1);  // m
			this.fields[ 9].Color = Drawing.Color.FromRGB(1,1,0);  // y
			this.fields[10].Color = Drawing.Color.FromRGB(0,0,0);  // k
			this.fields[10].BackColor = Drawing.Color.FromRGB(1,1,1);
			ToolTip.Default.SetToolTip(this.fields[ 7], Res.Strings.ColorSelector.LongCyan);
			ToolTip.Default.SetToolTip(this.fields[ 8], Res.Strings.ColorSelector.LongMagenta);
			ToolTip.Default.SetToolTip(this.fields[ 9], Res.Strings.ColorSelector.LongYellow);
			ToolTip.Default.SetToolTip(this.fields[10], Res.Strings.ColorSelector.LongBlack);

			this.fields[11].Color = Drawing.Color.FromRGB(1,1,1);  // g
			this.fields[11].BackColor = Drawing.Color.FromRGB(0,0,0);
			ToolTip.Default.SetToolTip(this.fields[11], Res.Strings.ColorSelector.LongGray);

			this.labels[ 0].Text = Res.Strings.ColorSelector.ShortRed;
			this.labels[ 1].Text = Res.Strings.ColorSelector.ShortGreen;
			this.labels[ 2].Text = Res.Strings.ColorSelector.ShortBlue;
			this.labels[ 3].Text = Res.Strings.ColorSelector.ShortAlpha;
			this.labels[ 4].Text = Res.Strings.ColorSelector.ShortHue;
			this.labels[ 5].Text = Res.Strings.ColorSelector.ShortSaturation;
			this.labels[ 6].Text = Res.Strings.ColorSelector.ShortValue;
			this.labels[ 7].Text = Res.Strings.ColorSelector.ShortCyan;
			this.labels[ 8].Text = Res.Strings.ColorSelector.ShortMagenta;
			this.labels[ 9].Text = Res.Strings.ColorSelector.ShortYellow;
			this.labels[10].Text = Res.Strings.ColorSelector.ShortBlack;
			this.labels[11].Text = Res.Strings.ColorSelector.ShortGray;

			this.circle = new ColorWheel(this);
			this.circle.Changed += new Support.EventHandler(this.HandleCircleChanged);

			this.palette = new ColorPalette(this);
			this.palette.HasOptionButton = true;
			this.palette.Export += new Support.EventHandler(this.HandlePaletteExport);
			this.palette.Import += new Support.EventHandler(this.HandlePaletteImport);

			this.picker = new Tools.Magnifier.DragSource(this);
			this.picker.HotColorChanged += new Support.EventHandler(this.HandlePickerHotColorChanged);
			ToolTip.Default.SetToolTip(this.picker, Res.Strings.ColorSelector.Picker);

			this.buttonClose = new GlyphButton(this);
			this.buttonClose.GlyphShape = GlyphShape.Close;
			this.buttonClose.ButtonStyle = ButtonStyle.Normal;
			this.buttonClose.Clicked += new MessageEventHandler(this.HandleButtonCloseClicked);
			ToolTip.Default.SetToolTip(this.buttonClose, Res.Strings.ColorSelector.Close);

			this.buttonRGB = new IconButton(this);
			this.buttonRGB.IconName = "manifest:Epsitec.Common.Widgets.Images.ColorSpaceRGB.icon";
			this.buttonRGB.Clicked += new MessageEventHandler(this.HandleButtonRGBClicked);
			ToolTip.Default.SetToolTip(this.buttonRGB, Res.Strings.ColorSelector.ColorSpace.RGB);

			this.buttonCMYK = new IconButton(this);
			this.buttonCMYK.IconName = "manifest:Epsitec.Common.Widgets.Images.ColorSpaceCMYK.icon";
			this.buttonCMYK.Clicked += new MessageEventHandler(this.HandleButtonCMYKClicked);
			ToolTip.Default.SetToolTip(this.buttonCMYK, Res.Strings.ColorSelector.ColorSpace.CMYK);

			this.buttonGray = new IconButton(this);
			this.buttonGray.IconName = "manifest:Epsitec.Common.Widgets.Images.ColorSpaceGray.icon";
			this.buttonGray.Clicked += new MessageEventHandler(this.HandleButtonGrayClicked);
			ToolTip.Default.SetToolTip(this.buttonGray, Res.Strings.ColorSelector.ColorSpace.Gray);
		}
		
		public ColorSelector(Widget embedder) : this()
		{
			this.SetEmbedder(embedder);
		}
		
		
		public override double					DefaultHeight
		{
			get
			{
				return 221;
			}
		}

		public Drawing.RichColor				Color
		{
			get
			{
				return this.color;
			}

			set
			{
				if ( this.color != value )
				{
					System.Diagnostics.Debug.Assert(this.suspendColorEvents == false);
					
					this.suspendColorEvents = true;
					this.color = value;
					this.circle.Color = value;
					this.UpdateColors();
					this.suspendColorEvents = false;
				}
			}
		}

		public ColorPalette						ColorPalette
		{
			get
			{
				return this.palette;
			}

			set
			{
				if ( this.palette != value )
				{
					if ( this.palette != null )
					{
						this.palette.Dispose();
					}

					this.palette = value;
				}
			}
		}
		
		public bool								HasCloseButton
		{
			get
			{
				return this.hasCloseButton;
			}

			set
			{
				if ( this.hasCloseButton != value )
				{
					this.hasCloseButton = value;
					this.UpdateClientGeometry();
				}
			}
		}


		// Met tout � jour apr�s un changement de couleur.
		protected void UpdateColors()
		{
			System.Diagnostics.Debug.Assert(this.suspendColorEvents == true);
			this.ColorToFieldsRGB();
			this.ColorToFieldsHSV();
			this.ColorToFieldsCMYK();
			this.ColorToFieldsGray();
			this.UpdateColorSpace();
			this.UpdateClientGeometry();
			this.Invalidate();
			this.OnChanged();
		}

		// Met � jour les boutons pour l'espace de couleur.
		protected void UpdateColorSpace()
		{
			Drawing.ColorSpace cs = this.color.ColorSpace;

			this.buttonRGB .ActiveState = (cs == Drawing.ColorSpace.RGB ) ? WidgetState.ActiveYes : WidgetState.ActiveNo;
			this.buttonCMYK.ActiveState = (cs == Drawing.ColorSpace.CMYK) ? WidgetState.ActiveYes : WidgetState.ActiveNo;
			this.buttonGray.ActiveState = (cs == Drawing.ColorSpace.Gray) ? WidgetState.ActiveYes : WidgetState.ActiveNo;
		}

		// Couleur -> textes �ditables.
		protected void ColorToFieldsRGB()
		{
			double a,r,g,b;
			this.color.Basic.GetARGB(out a, out r, out g, out b);
		
			this.fields[0].Value = (decimal) System.Math.Floor(r*255+0.5);
			this.fields[1].Value = (decimal) System.Math.Floor(g*255+0.5);
			this.fields[2].Value = (decimal) System.Math.Floor(b*255+0.5);
			this.fields[3].Value = (decimal) System.Math.Floor(a*255+0.5);
		}

		// Couleur -> textes �ditables.
		protected void ColorToFieldsHSV()
		{
			double h,s,v;
			this.circle.GetHSV(out h, out s, out v);
		
			this.fields[4].Value = (decimal) System.Math.Floor(System.Math.Floor(h+0.5));
			this.fields[5].Value = (decimal) System.Math.Floor(s*100+0.5);
			this.fields[6].Value = (decimal) System.Math.Floor(v*100+0.5);
		
			this.ColoriseSliders();
		}

		// Couleur -> textes �ditables.
		protected void ColorToFieldsCMYK()
		{
			double a = this.color.A;
			double c = this.color.C;
			double m = this.color.M;
			double y = this.color.Y;
			double k = this.color.K;
		
			this.fields[ 3].Value = (decimal) System.Math.Floor(a*255+0.5);
			this.fields[ 7].Value = (decimal) System.Math.Floor(c*100+0.5);
			this.fields[ 8].Value = (decimal) System.Math.Floor(m*100+0.5);
			this.fields[ 9].Value = (decimal) System.Math.Floor(y*100+0.5);
			this.fields[10].Value = (decimal) System.Math.Floor(k*100+0.5);
		}

		// Couleur -> textes �ditables.
		protected void ColorToFieldsGray()
		{
			double g = this.color.Gray;
		
			this.fields[11].Value = (decimal) System.Math.Floor(g*100+0.5);
		}

		// Textes �ditables RGB -> couleur.
		protected void FieldsRGBToColor()
		{
			double r = (double) this.fields[0].Value/255;
			double g = (double) this.fields[1].Value/255;
			double b = (double) this.fields[2].Value/255;
			double a = (double) this.fields[3].Value/255;

			System.Diagnostics.Debug.Assert(this.suspendColorEvents == false);
			this.suspendColorEvents = true;
			this.color = Drawing.RichColor.FromARGB(a,r,g,b);
			this.circle.Color = this.color;
			this.ColorToFieldsHSV();
			this.ColorToFieldsCMYK();
			this.ColorToFieldsGray();
			this.ColoriseSliders();
			this.Invalidate();
			this.OnChanged();
			this.suspendColorEvents = false;
		}

		// Textes �ditables HSV -> couleur.
		protected void FieldsHSVToColor()
		{
			double h = (double) this.fields[4].Value;
			double s = (double) this.fields[5].Value/100;
			double v = (double) this.fields[6].Value/100;

			System.Diagnostics.Debug.Assert(this.suspendColorEvents == false);
			this.suspendColorEvents = true;
			this.circle.SetHSV(h,s,v);
			this.color = Drawing.RichColor.FromAHSV(this.color.A, h,s,v);
			this.ColorToFieldsRGB();
			this.ColorToFieldsCMYK();
			this.ColorToFieldsGray();
			this.ColoriseSliders();
			this.Invalidate();
			this.OnChanged();
			this.suspendColorEvents = false;
		}

		// Textes �ditables CMYK -> couleur.
		protected void FieldsCMYKToColor()
		{
			double a = (double) this.fields[ 3].Value/255;
			double c = (double) this.fields[ 7].Value/100;
			double m = (double) this.fields[ 8].Value/100;
			double y = (double) this.fields[ 9].Value/100;
			double k = (double) this.fields[10].Value/100;

			System.Diagnostics.Debug.Assert(this.suspendColorEvents == false);
			this.suspendColorEvents = true;
			this.color = Drawing.RichColor.FromACMYK(a,c,m,y,k);
			this.circle.Color = this.color;
			this.ColorToFieldsRGB();
			this.ColorToFieldsHSV();
			this.ColorToFieldsGray();
			this.ColoriseSliders();
			this.Invalidate();
			this.OnChanged();
			this.suspendColorEvents = false;
		}

		// Textes �ditables Gray -> couleur.
		protected void FieldsGrayToColor()
		{
			double g = (double) this.fields[11].Value/100;

			System.Diagnostics.Debug.Assert(this.suspendColorEvents == false);
			this.suspendColorEvents = true;
			this.color = Drawing.RichColor.FromAGray(this.color.A, g);
			this.circle.SetGray(g);
			this.ColorToFieldsRGB();
			this.ColorToFieldsHSV();
			this.ColorToFieldsCMYK();
			this.ColoriseSliders();
			this.Invalidate();
			this.OnChanged();
			this.suspendColorEvents = false;
		}

		// Colorise certains sliders en fonction de la couleur d�finie.
		protected void ColoriseSliders()
		{
			double h,s,v;
			this.circle.GetHSV(out h, out s, out v);
			Drawing.Color saturated = Drawing.Color.FromHSV(h,1,1);

			this.fields[4].Color = saturated;

			if ( s == 0.0 )  // couleur grise ?
			{
				saturated = Drawing.Color.FromBrightness(1.0);  // blanc
			}

			this.fields[5].Color = saturated;
			this.fields[6].Color = saturated;
		}


		// Met � jour la g�om�trie.
		protected override void UpdateClientGeometry()
		{
			base.UpdateClientGeometry();

			if ( this.fields == null )  return;

			Drawing.Rectangle rect = this.Client.Bounds;
			rect.Deflate(1);
			double hCircle = rect.Height-5-20*3;
			hCircle = System.Math.Min(hCircle, rect.Width);
			Drawing.Rectangle r = new Drawing.Rectangle();

			bool visibleCircle = ( rect.Height > 160 );
			bool visibleFields = ( rect.Height > 3*20 );

			bool visibleFieldsRGB  = visibleFields;
			bool visibleFieldsCMYK = visibleFields;
			bool visibleFieldsGray = visibleFields;

			if ( this.Color.ColorSpace == Drawing.ColorSpace.RGB  )
			{
				visibleFieldsCMYK = false;
				visibleFieldsGray = false;
			}
			
			if ( this.Color.ColorSpace == Drawing.ColorSpace.CMYK )
			{
				visibleFieldsRGB  = false;
				visibleFieldsGray = false;
			}
			
			if ( this.Color.ColorSpace == Drawing.ColorSpace.Gray )
			{
				visibleFieldsRGB  = false;
				visibleFieldsCMYK = false;
			}

			r.Left   = rect.Left;
			r.Right  = rect.Left + hCircle;
			r.Bottom = rect.Top - hCircle;
			r.Top    = rect.Top;
			this.circle.Bounds = r;
			this.circle.SetVisible(visibleCircle);

			double dx = System.Math.Floor((rect.Width-hCircle-10)/this.palette.Columns);
			if ( dx > 4 )
			{
				double dy = System.Math.Floor(hCircle/(this.palette.Rows));
				dx = System.Math.Min(dx, dy)+1;

				r.Left = rect.Right-(dx*this.palette.Columns-1)+1-15;
				r.Right = rect.Right+1;
				r.Top = rect.Top;
				r.Bottom = rect.Top-(dx*this.palette.Rows-1);
				this.palette.Bounds = r;
				this.palette.Show();
			}
			else
			{
				this.palette.Hide();
			}

			r.Top    = rect.Bottom+3*19;
			r.Bottom = r.Top-20;
			for ( int i=0 ; i<=2 ; i++ )  // r,g,b
			{
				r.Left  = 10;
				r.Width = 12;
				this.labels[i].Bounds = r;
				this.labels[i].SetVisible(visibleFieldsRGB);

				r.Left  = r.Right;
				r.Width = 50;
				this.fields[i].Bounds = r;
				this.fields[i].SetVisible(visibleFieldsRGB);

				r.Offset(0, -19);
			}

			r.Top    = rect.Bottom+3*19;
			r.Bottom = r.Top-20;
			for ( int i=4 ; i<=6 ; i++ )  // t,s,i
			{
				r.Left  = 10+70;
				r.Width = 12;
				this.labels[i].Bounds = r;
				this.labels[i].SetVisible(visibleFieldsRGB);

				r.Left  = r.Right;
				r.Width = 50;
				this.fields[i].Bounds = r;
				this.fields[i].SetVisible(visibleFieldsRGB);

				r.Offset(0, -19);
			}
			
			r.Top    = rect.Bottom+3*19;
			r.Bottom = r.Top-20;
			for ( int i=7 ; i<=9 ; i++ )  // c,m,y
			{
				r.Left  = 10;
				r.Width = 12;
				this.labels[i].Bounds = r;
				this.labels[i].SetVisible(visibleFieldsCMYK);

				r.Left  = r.Right;
				r.Width = 50;
				this.fields[i].Bounds = r;
				this.fields[i].SetVisible(visibleFieldsCMYK);

				r.Offset(0, -19);
			}

			r.Top    = rect.Bottom+1*19;
			r.Bottom = r.Top-20;
			r.Left  = 10+70;
			r.Width = 12;
			this.labels[10].Bounds = r;
			this.labels[10].SetVisible(visibleFieldsCMYK);

			r.Left  = r.Right;
			r.Width = 50;
			this.fields[10].Bounds = r;
			this.fields[10].SetVisible(visibleFieldsCMYK);

			r.Top    = rect.Bottom+3*19;
			r.Bottom = r.Top-20;
			r.Left  = 10;
			r.Width = 12;
			this.labels[11].Bounds = r;
			this.labels[11].SetVisible(visibleFieldsGray);

			r.Left  = r.Right;
			r.Width = 50;
			this.fields[11].Bounds = r;
			this.fields[11].SetVisible(visibleFieldsGray);

			r.Top    = rect.Bottom+3*19;
			r.Bottom = r.Top-20;
			for ( int i=3 ; i<=3 ; i++ )  // a
			{
				r.Left  = 10+70+70;
				r.Width = 12;
				this.labels[i].Bounds = r;
				this.labels[i].SetVisible(visibleFields);

				r.Left  = r.Right;
				r.Width = 50;
				this.fields[i].Bounds = r;
				this.fields[i].SetVisible(visibleFields);

				r.Offset(0, -19);
			}
			
			r.Top    = r.Top-2;
			r.Bottom = r.Bottom+1;
			r.Right = this.fields[3].Right;
			r.Left  = r.Right - r.Height;
			this.picker.Bounds = r;
			this.picker.SetVisible(visibleFields);

			if ( this.hasCloseButton )
			{
				r.Left = rect.Left;
				r.Width = 14;
				r.Bottom = rect.Top-14;
				r.Top = rect.Top;
				this.buttonClose.Bounds = r;
				this.buttonClose.SetVisible(true);
			}
			else
			{
				this.buttonClose.SetVisible(false);
			}

#if false
			r.Left = rect.Left;
			r.Width = 14;
			r.Bottom = rect.Top-hCircle+14;
			r.Height = 14;
			this.buttonCMYK.Bounds = r;
			r.Offset(0, -14);
			this.buttonRGB.Bounds = r;
			r.Offset(14, 0);
			this.buttonGray.Bounds = r;
#else
			r.Left = rect.Left;
			r.Width = 14;
			r.Bottom = rect.Top-hCircle;
			r.Height = 14;
			this.buttonRGB.Bounds = r;
			r.Offset(14, 0);
			this.buttonCMYK.Bounds = r;
			r.Offset(14, 0);
			this.buttonGray.Bounds = r;
#endif
		}
		
		protected override void PaintBackgroundImplementation(Drawing.Graphics graphics, Drawing.Rectangle clipRect)
		{
			if ( !this.BackColor.IsEmpty )
			{
				graphics.AddFilledRectangle(this.Client.Bounds);
				graphics.RenderSolid(this.BackColor);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if ( disposing )
			{
				for ( int i=0 ; i<this.nbField ; i++ )
				{
					if ( i < 4 )
					{
						this.fields[i].TextChanged -= new Support.EventHandler(this.HandleTextRGBChanged);
					}
					else if ( i == 4 )
					{
						this.fields[i].TextChanged -= new Support.EventHandler(this.HandleTextHSVChanged);
					}
					else if ( i < 7 )
					{
						this.fields[i].TextChanged -= new Support.EventHandler(this.HandleTextHSVChanged);
					}
					else if ( i < 11 )
					{
						this.fields[i].TextChanged -= new Support.EventHandler(this.HandleTextCMYKChanged);
					}
					else
					{
						this.fields[i].TextChanged -= new Support.EventHandler(this.HandleTextGrayChanged);
					}
				}
				
				if ( this.circle != null )
				{
					this.circle.Changed -= new Support.EventHandler(this.HandleCircleChanged);
				}

				if ( this.palette != null )
				{
					this.palette.Export -= new Support.EventHandler(this.HandlePaletteExport);
					this.palette.Import -= new Support.EventHandler(this.HandlePaletteImport);
				}

				if ( this.picker != null )
				{
					this.picker.HotColorChanged -= new Support.EventHandler(this.HandlePickerHotColorChanged);
				}
			}
			
			base.Dispose(disposing);
		}

		
		private void HandlePickerHotColorChanged(object sender)
		{
			this.Color = new Drawing.RichColor(this.picker.HotColor);
		}
		
		// Une valeur RGB a �t� chang�e.
		private void HandleTextRGBChanged(object sender)
		{
			if ( !this.suspendColorEvents )
			{
				this.FieldsRGBToColor();
			}
		}

		// Une valeur HSV a �t� chang�e.
		private void HandleTextHSVChanged(object sender)
		{
			if ( !this.suspendColorEvents )
			{
				this.FieldsHSVToColor();
			}
		}

		// Une valeur CMYK a �t� chang�e.
		private void HandleTextCMYKChanged(object sender)
		{
			if ( !this.suspendColorEvents )
			{
				this.FieldsCMYKToColor();
			}
		}

		// Une valeur Gray a �t� chang�e.
		private void HandleTextGrayChanged(object sender)
		{
			if ( !this.suspendColorEvents )
			{
				this.FieldsGrayToColor();
			}
		}

		// Couleur dans le cercle chang�e.
		private void HandleCircleChanged(object sender)
		{
			if ( !this.suspendColorEvents )
			{
				this.suspendColorEvents = true;
				this.color = this.circle.Color;
				this.ColorToFieldsRGB();
				this.ColorToFieldsHSV();
				this.ColorToFieldsCMYK();
				this.ColorToFieldsGray();
				this.ColoriseSliders();
				this.Invalidate();
				this.OnChanged();
				this.suspendColorEvents = false;
			}
		}

		// Couleur dans palette cliqu�e.
		private void HandlePaletteExport(object sender)
		{
			this.Color = this.palette.Color;
			this.OnChanged();
		}

		// Couleur dans palette cliqu�e.
		private void HandlePaletteImport(object sender)
		{
			this.palette.Color = this.Color;
		}

		private void HandleButtonCloseClicked(object sender, MessageEventArgs e)
		{
			this.OnCloseClicked();
		}

		private void HandleButtonRGBClicked(object sender, MessageEventArgs e)
		{
			this.color.ColorSpace = Drawing.ColorSpace.RGB;
			this.circle.Color = this.color;

			this.suspendColorEvents = true;
			this.UpdateColors();
			this.suspendColorEvents = false;
		}

		private void HandleButtonCMYKClicked(object sender, MessageEventArgs e)
		{
			this.color.ColorSpace = Drawing.ColorSpace.CMYK;
			this.circle.Color = this.color;

			this.suspendColorEvents = true;
			this.UpdateColors();
			this.suspendColorEvents = false;
		}

		private void HandleButtonGrayClicked(object sender, MessageEventArgs e)
		{
			this.color.ColorSpace = Drawing.ColorSpace.Gray;
			this.circle.Color = this.color;

			this.suspendColorEvents = true;
			this.UpdateColors();
			this.suspendColorEvents = false;
		}

		
		// G�n�re un �v�nement pour dire �a a chang�.
		protected virtual void OnChanged()
		{
			if ( this.Changed != null )  // qq'un �coute ?
			{
				this.Changed(this);
			}
		}

		protected virtual void OnCloseClicked()
		{
			if ( this.CloseClicked != null )
			{
				this.CloseClicked(this);
			}
		}
		
		
		public event Support.EventHandler		Changed;
		public event Support.EventHandler		CloseClicked;

		protected Drawing.RichColor				color = new Drawing.RichColor(0.0);
		protected ColorWheel					circle;
		protected ColorPalette					palette;
		protected int							nbField;
		protected StaticText[]					labels;
		protected TextFieldSlider[]				fields;
		protected bool							suspendColorEvents = false;
		protected bool							hasCloseButton = false;
		protected GlyphButton					buttonClose;
		protected IconButton					buttonRGB;
		protected IconButton					buttonCMYK;
		protected IconButton					buttonGray;
		private Tools.Magnifier.DragSource		picker;
	}
}
