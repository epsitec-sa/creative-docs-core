//	Copyright � 2003-2007, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;

namespace Epsitec.App.Dolphin.Components
{
	/// <summary>
	/// Petit processeur 8 bits bien orthogonal.
	/// </summary>
	public class TinyProcessor : AbstractProcessor
	{
		protected enum Instructions
		{
			//	r     =  A,B,X,Y
			//	r'    =  A,B
			//	ADDR  =  adresse 16 bits, 4 bits de mode (abs/rel, +{XY}) et 12 bits valeur
			//	#val  =  valeur absolue 8 bits

			Nop     = 0x00,
			Ret     = 0x01,
			Halt    = 0x02,
			SetC    = 0x04,
			ClrC    = 0x05,
			SetV    = 0x06,
			ClrV    = 0x07,

			PushR   = 0x08,		// PUSH r
			PopR    = 0x0C,		// POP r

			Jump    = 0x10,
			JumpEQ  = 0x12,
			JumpNE  = 0x13,
			JumpLO  = 0x14,
			JumpLS  = 0x15,
			JumpHI  = 0x16,
			JumpHS  = 0x17,
			JumpVC  = 0x18,
			JumpVS  = 0x19,
			JumpNC  = 0x1A,
			JumpNS  = 0x1B,

			//	0x1C..0x1F libre

			Call    = 0x20,
			CallEQ  = 0x22,
			CallNE  = 0x23,
			CallLO  = 0x24,
			CallLS  = 0x25,
			CallHI  = 0x26,
			CallHS  = 0x27,
			CallVC  = 0x28,
			CallVS  = 0x29,
			CallNC  = 0x2A,
			CallNS  = 0x2B,

			// 0x2C..02F libre
			// 0x30..03F libre

			ClrR    = 0x40,		// op r
			IncR    = 0x48,
			DecR    = 0x4C,

			RlRR    = 0x50,		// op r'
			RrRR    = 0x52,
			RlcRR   = 0x54,
			RrcRR   = 0x56,
			NotRR   = 0x58,

			ClrA    = 0x60,		// op ADDR
			IncA    = 0x61,
			DecA    = 0x62,
			RlA     = 0x64,
			RrA     = 0x65,
			RlcA    = 0x66,
			RrcA    = 0x67,
			NotA    = 0x68,

			//	0x70..0x7F libre

			MoveAR  = 0x80,		// op ADDR,r
			CompAR  = 0x84,
			AddAR   = 0x88,
			SubAR   = 0x8C,

			AndARR  = 0x90,		// op ADDR,r'
			OrARR   = 0x92,
			XorARR  = 0x94,
			TestARR = 0x98,
			TClrARR = 0x9A,
			TSetARR = 0x9C,
			TInvARR = 0x9D,

			MoveRA  = 0xA0,		// op r,ADDR
			CompRA  = 0xA4,
			AddRA   = 0xA8,
			SubRA   = 0xAC,

			AndRRA  = 0xB0,		// op r',ADDR
			OrRRA   = 0xB2,
			XorRRA  = 0xB4,
			TestRRA = 0xB8,
			TClrRRA = 0xBA,
			TSetRRA = 0xBC,
			TInvRRA = 0xBD,

			MoveVR  = 0xC0,		// op #val,r
			CompVR  = 0xC4,
			AddVR   = 0xC8,
			SubVR   = 0xCC,

			AndVRR  = 0xD0,		// op #val,r'
			OrVRR   = 0xD2,
			XorVRR  = 0xD4,
			TestVRR = 0xD8,
			TClrVRR = 0xDA,
			TSetVRR = 0xDC,
			TInvVRR = 0xDD,

			MoveRR  = 0xE0,		// MOVE r,r

			//	0xF0..0xFF libre
		}


		public TinyProcessor(Memory memory) : base(memory)
		{
			//	Constructeur du processeur.
		}

		public override string Name
		{
			//	Nom du processeur.
			get
			{
				return "Tiny";
			}
		}

		public override void Reset()
		{
			//	Reset du processeur pour d�marrer � l'adresse 0.
			base.Reset();
			this.registerPC = Memory.RamBase;
			this.registerSP = Memory.StackBase;
			this.registerF = 0;
			this.registerA = 0;
			this.registerB = 0;
			this.registerX = 0;
			this.registerY = 0;
		}

		public override bool IsCall(out int retAddress)
		{
			//	Indique si le processeur est sur une instruction CALL.
			//	Si oui, retourne l'adresse apr�s le CALL.
			Instructions op = (Instructions) this.memory.Read(this.registerPC);

			if (op >= Instructions.Call && op <= Instructions.CallNS)
			{
				retAddress = this.registerPC+3;
				return true;
			}
			else
			{
				retAddress = 0;
				return false;
			}
		}

		public override void Clock()
		{
			//	Ex�cute une instruction du processeur.
			if (this.isHalted)
			{
				return;
			}

			if (this.registerPC < 0 || this.registerPC >= this.memory.Length)
			{
				this.Reset();
			}

			int op = this.memory.Read(this.registerPC++);
			int data, address;

			switch ((Instructions) op)
			{
				case Instructions.Nop:
					return;

				case Instructions.Ret:
					this.registerPC = this.StackPopWord();
					return;

				case Instructions.Halt:
					this.registerPC--;
					this.isHalted = true;
					return;

				case Instructions.SetC:
					this.SetFlag(TinyProcessor.FlagCarry, true);
					return;

				case Instructions.ClrC:
					this.SetFlag(TinyProcessor.FlagCarry, false);
					return;

				case Instructions.SetV:
					this.SetFlag(TinyProcessor.FlagOverflow, true);
					return;

				case Instructions.ClrV:
					this.SetFlag(TinyProcessor.FlagOverflow, false);
					return;
			}

			if (op >= (int) Instructions.PushR && op <= (int) Instructions.PushR + 0x03)  // PUSH r
			{
				int n = op & 0x03;
				this.StackPushByte(this.GetRegister(n));
				return;
			}

			if (op >= (int) Instructions.PopR && op <= (int) Instructions.PopR + 0x03)  // POP r
			{
				int n = op & 0x03;
				this.SetRegister(n, this.StackPopByte());
				return;
			}

			if (op >= (int) Instructions.Jump && op <= (int) Instructions.JumpNS)
			{
				address = this.AddressAbs;
				if (this.IsTestTrue(op))
				{
					this.registerPC = address;
				}
				return;
			}

			if (op >= (int) Instructions.Call && op <= (int) Instructions.CallNS)
			{
				address = this.AddressAbs;
				if (this.IsTestTrue(op))
				{
					this.StackPushWord(this.registerPC);
					this.registerPC = address;
				}
				return;
			}

			if (op >= (int) Instructions.ClrR && op <= (int) Instructions.ClrR + 0x0F)  // op r
			{
				int n = op & 0x03;
				Instructions i = (Instructions) (op & 0xFC);

				switch (i)
				{
					case Instructions.ClrR:
						data = 0;
						this.SetRegister(n, data);
						this.SetFlagsOper(data);
						return;

					case Instructions.IncR:
						data = this.GetRegister(n) + 1;
						this.SetRegister(n, data);
						this.SetFlagsOper(data);
						return;

					case Instructions.DecR:
						data = this.GetRegister(n) - 1;
						this.SetRegister(n, data);
						this.SetFlagsOper(data);
						return;
				}
			}

			if (op >= (int) Instructions.RlRR && op <= (int) Instructions.RlRR + 0x0F)  // op r'
			{
				int n = op & 0x01;
				Instructions i = (Instructions) (op & 0xFE);

				switch (i)
				{
					case Instructions.RlRR:
						data = this.RotateLeft(this.GetRegister(n), false);
						this.SetRegister(n, data);
						this.SetFlagsOper(data);
						return;

					case Instructions.RrRR:
						data = this.RotateRight(this.GetRegister(n), false);
						this.SetRegister(n, data);
						this.SetFlagsOper(data);
						return;

					case Instructions.RlcRR:
						data = this.RotateLeft(this.GetRegister(n), true);
						this.SetRegister(n, data);
						this.SetFlagsOper(data);
						return;

					case Instructions.RrcRR:
						data = this.RotateRight(this.GetRegister(n), true);
						this.SetRegister(n, data);
						this.SetFlagsOper(data);
						return;

					case Instructions.NotRR:
						data = this.GetRegister(n) ^ 0xFF;
						this.SetRegister(n, data);
						this.SetFlagsOper(data);
						return;
				}
			}

			if (op >= (int) Instructions.ClrA && op <= (int) Instructions.ClrA + 0x0F)  // op ADDR
			{
				Instructions i = (Instructions) op;
				address = this.AddressAbs;

				switch (i)
				{
					case Instructions.ClrA:
						data = 0;
						this.memory.Write(address, data);
						this.SetFlagsOper(data);
						return;

					case Instructions.IncA:
						data = this.memory.Read(address) + 1;
						this.memory.Write(address, data);
						this.SetFlagsOper(data);
						return;

					case Instructions.DecA:
						data = this.memory.Read(address) - 1;
						this.memory.Write(address, data);
						this.SetFlagsOper(data);
						return;

					case Instructions.RlA:
						data = this.RotateLeft(this.memory.Read(address), false);
						this.memory.Write(address, data);
						this.SetFlagsOper(data);
						return;

					case Instructions.RrA:
						data = this.RotateRight(this.memory.Read(address), false);
						this.memory.Write(address, data);
						this.SetFlagsOper(data);
						return;

					case Instructions.RlcA:
						data = this.RotateLeft(this.memory.Read(address), true);
						this.memory.Write(address, data);
						this.SetFlagsOper(data);
						return;

					case Instructions.RrcA:
						data = this.RotateRight(this.memory.Read(address), true);
						this.memory.Write(address, data);
						this.SetFlagsOper(data);
						return;

					case Instructions.NotA:
						data = this.memory.Read(address) ^ 0xFF;
						this.memory.Write(address, data);
						this.SetFlagsOper(data);
						return;
				}
			}

			if (op >= (int) Instructions.MoveAR && op <= (int) Instructions.MoveAR + 0x0F)  // op ADDR,r
			{
				int n = op & 0x03;
				Instructions i = (Instructions) (op & 0xFC);
				address = this.AddressAbs;

				switch (i)
				{
					case Instructions.MoveAR:
						data = this.memory.Read(address);
						this.SetRegister(n, data);
						this.SetFlagsOper(data);
						return;

					case Instructions.CompAR:
						this.SetFlagsCompare(this.GetRegister(n), this.memory.Read(address));
						return;

					case Instructions.AddAR:
						data = this.GetRegister(n) + this.memory.Read(address);
						this.SetRegister(n, data);
						this.SetFlagsOper(data);
						return;

					case Instructions.SubAR:
						data = this.GetRegister(n) - this.memory.Read(address);
						this.SetRegister(n, data);
						this.SetFlagsOper(data);
						return;
				}
			}

			if (op >= (int) Instructions.AndARR && op <= (int) Instructions.AndARR + 0x0F)  // op ADDR,r'
			{
				int n = op & 0x01;
				Instructions i = (Instructions) (op & 0xFE);
				address = this.AddressAbs;

				switch (i)
				{
					case Instructions.AndARR:
						data = this.GetRegister(n) & this.memory.Read(address);
						this.SetRegister(n, data);
						this.SetFlagsOper(data);
						return;

					case Instructions.OrARR:
						data = this.GetRegister(n) | this.memory.Read(address);
						this.SetRegister(n, data);
						this.SetFlagsOper(data);
						return;

					case Instructions.XorARR:
						data = this.GetRegister(n) ^ this.memory.Read(address);
						this.SetRegister(n, data);
						this.SetFlagsOper(data);
						return;

					case Instructions.TestARR:
						data = (1 << (this.memory.Read(address) & 0x07));
						this.SetFlag(TinyProcessor.FlagZero, (this.GetRegister(n) & data) == 0);
						return;

					case Instructions.TClrARR:
						data = (1 << (this.memory.Read(address) & 0x07));
						this.SetFlag(TinyProcessor.FlagZero, (this.GetRegister(n) & data) == 0);
						this.SetRegister(n, this.GetRegister(n) & ~data);
						return;

					case Instructions.TSetARR:
						data = (1 << (this.memory.Read(address) & 0x07));
						this.SetFlag(TinyProcessor.FlagZero, (this.GetRegister(n) & data) == 0);
						this.SetRegister(n, this.GetRegister(n) | data);
						return;

					case Instructions.TInvARR:
						data = (1 << (this.memory.Read(address) & 0x07));
						this.SetFlag(TinyProcessor.FlagZero, (this.GetRegister(n) & data) == 0);
						this.SetRegister(n, this.GetRegister(n) ^ data);
						return;
				}
			}

			if (op >= (int) Instructions.MoveRA && op <= (int) Instructions.MoveRA + 0x0F)  // op r,ADDR
			{
				int n = op & 0x03;
				Instructions i = (Instructions) (op & 0xFC);
				address = this.AddressAbs;

				switch (i)
				{
					case Instructions.MoveRA:
						data = this.GetRegister(n);
						this.memory.Write(address, data);
						this.SetFlagsOper(data);
						return;

					case Instructions.CompRA:
						this.SetFlagsCompare(this.memory.Read(address), this.GetRegister(n));
						return;

					case Instructions.AddRA:
						data = this.memory.Read(address) + this.GetRegister(n);
						this.memory.Write(address, data);
						this.SetFlagsOper(data);
						return;

					case Instructions.SubRA:
						data = this.memory.Read(address) - this.GetRegister(n);
						this.memory.Write(address, data);
						this.SetFlagsOper(data);
						return;
				}
			}

			if (op >= (int) Instructions.AndRRA && op <= (int) Instructions.AndRRA + 0x0F)  // op r',ADDR
			{
				int n = op & 0x01;
				Instructions i = (Instructions) (op & 0xFE);
				address = this.AddressAbs;

				switch (i)
				{
					case Instructions.AndRRA:
						data = this.memory.Read(address) & this.GetRegister(n);
						this.memory.Write(address, data);
						this.SetFlagsOper(data);
						return;

					case Instructions.OrRRA:
						data = this.memory.Read(address) | this.GetRegister(n);
						this.memory.Write(address, data);
						this.SetFlagsOper(data);
						return;

					case Instructions.XorRRA:
						data = this.memory.Read(address) ^ this.GetRegister(n);
						this.memory.Write(address, data);
						this.SetFlagsOper(data);
						return;

					case Instructions.TestRRA:
						data = (1 << (this.GetRegister(n) & 0x07));
						this.SetFlag(TinyProcessor.FlagZero, (this.memory.Read(address) & data) == 0);
						return;

					case Instructions.TClrRRA:
						data = (1 << (this.GetRegister(n) & 0x07));
						this.SetFlag(TinyProcessor.FlagZero, (this.memory.Read(address) & data) == 0);
						this.memory.Write(address, this.memory.Read(address) & ~data);
						return;

					case Instructions.TSetRRA:
						data = (1 << (this.GetRegister(n) & 0x07));
						this.SetFlag(TinyProcessor.FlagZero, (this.memory.Read(address) & data) == 0);
						this.memory.Write(address, this.memory.Read(address) | data);
						return;

					case Instructions.TInvRRA:
						data = (1 << (this.GetRegister(n) & 0x07));
						this.SetFlag(TinyProcessor.FlagZero, (this.memory.Read(address) & data) == 0);
						this.memory.Write(address, this.memory.Read(address) ^ data);
						return;
				}
			}

			if (op >= (int) Instructions.MoveVR && op <= (int) Instructions.MoveVR + 0x0F)  // op #val,r
			{
				int n = op & 0x03;
				Instructions i = (Instructions) (op & 0xFC);
				data = this.memory.Read(this.registerPC++);

				switch (i)
				{
					case Instructions.MoveVR:
						this.SetRegister(n, data);
						this.SetFlagsOper(data);
						return;

					case Instructions.CompVR:
						this.SetFlagsCompare(this.GetRegister(n), data);
						return;

					case Instructions.AddVR:
						data = this.GetRegister(n) + data;
						this.SetRegister(n, data);
						this.SetFlagsOper(data);
						return;

					case Instructions.SubVR:
						data = this.GetRegister(n) - data;
						this.SetRegister(n, data);
						this.SetFlagsOper(data);
						return;
				}
			}

			if (op >= (int) Instructions.AndVRR && op <= (int) Instructions.AndVRR + 0x0F)  // op #val,r'
			{
				int n = op & 0x01;
				Instructions i = (Instructions) (op & 0xFE);
				data = this.memory.Read(this.registerPC++);

				switch (i)
				{
					case Instructions.AndVRR:
						data = this.GetRegister(n) & data;
						this.SetRegister(n, data);
						this.SetFlagsOper(data);
						return;

					case Instructions.OrVRR:
						data = this.GetRegister(n) | data;
						this.SetRegister(n, data);
						this.SetFlagsOper(data);
						return;

					case Instructions.XorVRR:
						data = this.GetRegister(n) ^ data;
						this.SetRegister(n, data);
						this.SetFlagsOper(data);
						return;

					case Instructions.TestVRR:
						data = (1 << (data & 0x07));
						this.SetFlag(TinyProcessor.FlagZero, (this.GetRegister(n) & data) == 0);
						return;

					case Instructions.TClrVRR:
						data = (1 << (data & 0x07));
						this.SetFlag(TinyProcessor.FlagZero, (this.GetRegister(n) & data) == 0);
						this.SetRegister(n, this.GetRegister(n) & ~data);
						return;

					case Instructions.TSetVRR:
						data = (1 << (data & 0x07));
						this.SetFlag(TinyProcessor.FlagZero, (this.GetRegister(n) & data) == 0);
						this.SetRegister(n, this.GetRegister(n) | data);
						return;

					case Instructions.TInvVRR:
						data = (1 << (data & 0x07));
						this.SetFlag(TinyProcessor.FlagZero, (this.GetRegister(n) & data) == 0);
						this.SetRegister(n, this.GetRegister(n) ^ data);
						return;
				}
			}

			if (op >= (int) Instructions.MoveRR && op <= (int) Instructions.MoveRR + 0x0F)  // MOVE r,r
			{
				int src = (op>>2) & 0x03;
				int dst = op & 0x03;

				data = this.GetRegister(src);
				this.SetRegister(dst, data);
				this.SetFlagsOper(data);
				return;
			}
		}



		protected int GetRegister(int n)
		{
			//	Retourne le contenu d'un registre A,B,X,Y.
			n &= 0x03;

			switch (n)
			{
				case 0:
					return this.registerA;

				case 1:
					return this.registerB;

				case 2:
					return this.registerX;

				case 3:
					return this.registerY;
			}

			return 0;
		}

		protected void SetRegister(int n, int value)
		{
			//	Modifie le contenu d'un registre A,B,X,Y.
			n &= 0x03;

			switch (n)
			{
				case 0:
					this.registerA = value;
					break;

				case 1:
					this.registerB = value;
					break;

				case 2:
					this.registerX = value;
					break;

				case 3:
					this.registerY = value;
					break;
			}
		}


		protected int RotateRight(int value, bool withCarry)
		{
			bool bit = (value & 0x01) != 0;

			value = value >> 1;

			if (withCarry)
			{
				if (this.TestFlag(TinyProcessor.FlagCarry))
				{
					value |= 0x80;
				}
			}
			else
			{
				if (bit)
				{
					value |= 0x80;
				}
			}

			this.SetFlag(TinyProcessor.FlagCarry, bit);
			return value;
		}

		protected int RotateLeft(int value, bool withCarry)
		{
			bool bit = (value & 0x80) != 0;

			value = value << 1;

			if (withCarry)
			{
				if (this.TestFlag(TinyProcessor.FlagCarry))
				{
					value |= 0x01;
				}
			}
			else
			{
				if (bit)
				{
					value |= 0x01;
				}
			}

			this.SetFlag(TinyProcessor.FlagCarry, bit);
			return value;
		}


		protected void StackPushWord(int value)
		{
			this.memory.Write(this.DecSP, value & 0xff);
			this.memory.Write(this.DecSP, (value >> 8) & 0xff);
		}

		protected int StackPopWord()
		{
			int value = this.memory.Read(this.IncSP) << 8;
			value |= this.memory.Read(this.IncSP);
			return value;
		}

		protected void StackPushByte(int value)
		{
			this.memory.Write(this.DecSP, value);
		}

		protected int StackPopByte()
		{
			return this.memory.Read(this.IncSP);
		}

		protected int DecSP
		{
			get
			{
				this.registerSP--;
				this.registerSP &= 0x7ff;
				return this.registerSP;
			}
		}

		protected int IncSP
		{
			get
			{
				int sp = this.registerSP;
				this.registerSP++;
				this.registerSP &= 0x7ff;
				return sp;
			}
		}


		protected int AddressAbs
		{
			//	Lit ADDR qui suit une instruction, et g�re les diff�rents modes d'adressages.
			get
			{
				int mode = (this.memory.Read(this.registerPC++) << 8) | (this.memory.Read(this.registerPC++));
				int address = mode & 0x0FFF;

				if ((mode & 0x8000) != 0)  // relatif ?
				{
					if ((address & 0x0800) != 0)  // offset n�gatif ?
					{
						address = address-0x1000;
					}

					address = this.registerPC + address;
				}

				if ((mode & 0x4000) != 0)  // +{X} ou +{Y} ?
				{
					if ((mode & 0x1000) == 0)
					{
						address += this.registerX;
					}
					else
					{
						address += this.registerY;
					}
				}

				return address;
			}
		}


		protected void SetFlagsCompare(int a, int b)
		{
			this.SetFlag(TinyProcessor.FlagZero, a == b);
			this.SetFlag(TinyProcessor.FlagCarry, a >= b);
		}

		protected int SetFlagsOper(int value)
		{
			this.SetFlag(TinyProcessor.FlagZero, value == 0);
			this.SetFlag(TinyProcessor.FlagNeg, (value & 0x80) != 0);

			if ((value & 0x80) == 0)  // valeur positive ?
			{
				this.SetFlag(TinyProcessor.FlagOverflow, (value & 0xffffff00) != 0);
			}
			else  // valeur n�gative ?
			{
				this.SetFlag(TinyProcessor.FlagOverflow, (value & 0xffffff00) == 0);
			}

			return value & 0xff;
		}

		protected bool IsTestTrue(int op)
		{
			Instructions test = (Instructions) ((int) Instructions.Jump + (op & 0x0F));

			switch (test)
			{
				case Instructions.JumpEQ:
					return this.TestFlag(TinyProcessor.FlagZero);

				case Instructions.JumpNE:
					return !this.TestFlag(TinyProcessor.FlagZero);

				case Instructions.JumpLO:
					return !this.TestFlag(TinyProcessor.FlagZero) && !this.TestFlag(TinyProcessor.FlagCarry);

				case Instructions.JumpLS:
					return this.TestFlag(TinyProcessor.FlagZero) || !this.TestFlag(TinyProcessor.FlagCarry);

				case Instructions.JumpHI:
					return !this.TestFlag(TinyProcessor.FlagZero) && this.TestFlag(TinyProcessor.FlagCarry);

				case Instructions.JumpHS:
					return this.TestFlag(TinyProcessor.FlagZero) || this.TestFlag(TinyProcessor.FlagCarry);

				case Instructions.JumpVC:
					return !this.TestFlag(TinyProcessor.FlagOverflow);

				case Instructions.JumpVS:
					return this.TestFlag(TinyProcessor.FlagOverflow);

				case Instructions.JumpNC:
					return !this.TestFlag(TinyProcessor.FlagNeg);

				case Instructions.JumpNS:
					return this.TestFlag(TinyProcessor.FlagNeg);
			}

			return true;
		}

		protected void SetFlag(int flag, bool value)
		{
			this.registerF &= ~(1 << flag);

			if (value)
			{
				this.registerF |= (1 << flag);
			}
		}

		protected bool TestFlag(int flag)
		{
			return (this.registerF & (1 << flag)) != 0;
		}


		#region Register
		public override IEnumerable<string> RegisterNames
		{
			//	Enum�re tous les noms de registres.
			get
			{
				yield return "PC";
				yield return "SP";
				yield return "F";
				yield return "A";
				yield return "B";
				yield return "X";
				yield return "Y";
			}
		}

		public override int GetRegisterSize(string name)
		{
			//	Retourne la taille (nombre de bits) d'un registre.
			switch (name)
			{
				case "PC":
				case "SP":
					return Memory.TotalAddress;

				case "F":
				case "A":
				case "B":
				case "X":
				case "Y":
					return Memory.TotalData;
			}

			return base.GetRegisterSize(name);
		}

		public override string GetRegisterBitNames(string name)
		{
			//	Retourne les noms des bits d'un registre.
			if (name == "F")
			{
				return "CZNV";  // bits 0..7 !
			}

			return null;
		}

		public override int GetRegisterValue(string name)
		{
			//	Retourne la valeur d'un registre.
			switch (name)
			{
				case "PC":
					return this.registerPC;

				case "SP":
					return this.registerSP;

				case "F":
					return this.registerF;

				case "A":
					return this.registerA;

				case "B":
					return this.registerB;

				case "X":
					return this.registerX;

				case "Y":
					return this.registerY;
			}

			return base.GetRegisterValue(name);
		}

		public override void SetRegisterValue(string name, int value)
		{
			//	Modifie la valeur d'un registre.
			switch (name)
			{
				case "PC":
					this.registerPC = value;
					break;

				case "SP":
					this.registerSP = value;
					break;

				case "F":
					this.registerF = value;
					break;

				case "A":
					this.registerA = value;
					break;

				case "B":
					this.registerB = value;
					break;

				case "X":
					this.registerX = value;
					break;

				case "Y":
					this.registerY = value;
					break;
			}
		}
		#endregion


		#region Rom
		public override void RomInitialise(int address)
		{
			//	Rempli la Rom.
			int indirect = address;
			address += 3*64;  // place pour 64 appels
			this.RomWrite(ref indirect, ref address, TinyProcessor.WaitKey);
			this.RomWrite(ref indirect, ref address, TinyProcessor.DisplayBinaryDigit);
			this.RomWrite(ref indirect, ref address, TinyProcessor.DisplayHexaDigit);
			this.RomWrite(ref indirect, ref address, TinyProcessor.DisplayHexaByte);
			this.RomWrite(ref indirect, ref address, TinyProcessor.DisplayDecimal);
			this.RomWrite(ref indirect, ref address, TinyProcessor.SetPixel);
			this.RomWrite(ref indirect, ref address, TinyProcessor.ClrPixel);
		}

		protected void RomWrite(ref int indirect, ref int address, byte[] code)
		{
			this.memory.WriteRom(indirect++, (byte) Instructions.Jump);
			this.memory.WriteRom(indirect++, (address >> 8) & 0xff);
			this.memory.WriteRom(indirect++, address & 0xff);

			foreach (byte data in code)
			{
				this.memory.WriteRom(address++, data);
			}
		}

		//	Attend la pression d'une touche du clavier simul�.
		//	in	-
		//	out	A touche press�e
		//	mod	A
		protected static byte[] WaitKey =
		{
			(byte) Instructions.MoveAR+0, 0x0C, 0x07,	// MOVE C07,A		; lit le clavier
			(byte) Instructions.TClrVRR+0, 0x07,		// TCLR A:#7		; bit full ?
			(byte) Instructions.JumpEQ, 0x8F, 0xF8,		// JUMP,EQ R8^LOOP	; non, jump loop
			(byte) Instructions.Ret,					// RET
		};

		//	Affiche des segments � choix.
		//	in	A segments � allumer
		//		B digit 0..3
		//	out	-
		//	mod	-
		protected static byte[] DisplayBinaryDigit =
		{
			(byte) Instructions.PushR+1,				// PUSH B
			(byte) Instructions.PushR+2,				// PUSH X

			(byte) Instructions.AndVRR+1, 0x03,			// AND #03,B
			(byte) Instructions.MoveRR+0x6,				// MOVE B,X
			(byte) Instructions.MoveRA+0, 0x4C, 0x00,	// MOVE A,C00+{X}

			(byte) Instructions.PopR+2,					// POP X
			(byte) Instructions.PopR+1,					// POP B
			(byte) Instructions.Ret,					// RET
		};

		//	Affiche un digit hexad�cimal.
		//	in	A valeur 0..15
		//		B digit 0..3
		//	out	-
		//	mod	-
		protected static byte[] DisplayHexaDigit =
		{
			(byte) Instructions.PushR+0,				// PUSH A
			(byte) Instructions.PushR+1,				// PUSH B
			(byte) Instructions.PushR+2,				// PUSH X

			(byte) Instructions.MoveRR+0x2,				// MOVE A,X
			(byte) Instructions.MoveAR+0, 0xC0, 0x0A,	// MOVE R8^TABLE+{X},A

			(byte) Instructions.AndVRR+1, 0x03,			// AND #03,B
			(byte) Instructions.MoveRR+0x6,				// MOVE B,X
			(byte) Instructions.MoveRA+0, 0x4C, 0x00,	// MOVE A,C00+{X}

			(byte) Instructions.PopR+2,					// POP X
			(byte) Instructions.PopR+1,					// POP B
			(byte) Instructions.PopR+0,					// POP A
			(byte) Instructions.Ret,					// RET
														// TABLE:
			0x3F, 0x03, 0x6D, 0x67, 0x53, 0x76, 0x7E, 0x23, 0x7F, 0x77, 0x7B, 0x5E, 0x3C, 0x4F, 0x7C, 0x78,
		};

		//	Affiche un byte hexad�cimal sur deux digits.
		//	in	A valeur 0..255
		//		B premier digit 0..2
		//	out	-
		//	mod	-
		protected static byte[] DisplayHexaByte =
		{
			(byte) Instructions.Ret,				// RET
		};

		//	Affiche une valeur d�cimale.
		//	in	HL valeur
		//	out	-
		//	mod	-
		protected static byte[] DisplayDecimal =
		{
			(byte) Instructions.Ret,				// RET
		};

		//	Allume un pixel dans l'�cran bitmap.
		//	in	A coordonn�e X 0..31
		//		B coordonn�e Y 0..23
		//	out	-
		//	mod	-
		protected static byte[] SetPixel =
		{
			(byte) Instructions.Ret,				// RET
		};

		//	Eteint un pixel dans l'�cran bitmap.
		//	in	A coordonn�e X 0..31
		//		B coordonn�e Y 0..23
		//	out	-
		//	mod	-
		protected static byte[] ClrPixel =
		{
			(byte) Instructions.Ret,				// RET
		};
		#endregion


		#region Chapters
		public override List<string> HelpChapters
		{
			//	Retourne la liste des chapitres.
			get
			{
				List<string> chapters = new List<string>();
				
				chapters.Add("Intro");
				chapters.Add("Data");
				chapters.Add("Op");
				chapters.Add("Branch");
				chapters.Add("ROM");
				
				return chapters;
			}
		}

		public override string HelpChapter(string chapter)
		{
			//	Retourne le texte d'un chapitre.
			System.Text.StringBuilder builder = new System.Text.StringBuilder();

			switch (chapter)
			{
				case "Intro":
					AbstractProcessor.HelpPutTitle(builder, "Binaire et hexad�cimal");
					AbstractProcessor.HelpPutLine(builder, "(<i>d�cimal: binaire = hexa</i>)");
					AbstractProcessor.HelpPutLine(builder, "  0: 0000 = 0");
					AbstractProcessor.HelpPutLine(builder, "  1: 0001 = 1");
					AbstractProcessor.HelpPutLine(builder, "  2: 0010 = 2");
					AbstractProcessor.HelpPutLine(builder, "  3: 0011 = 3");
					AbstractProcessor.HelpPutLine(builder, "  4: 0100 = 4");
					AbstractProcessor.HelpPutLine(builder, "  5: 0101 = 5");
					AbstractProcessor.HelpPutLine(builder, "  6: 0110 = 6");
					AbstractProcessor.HelpPutLine(builder, "  7: 0111 = 7");
					AbstractProcessor.HelpPutLine(builder, "  8: 1000 = 8");
					AbstractProcessor.HelpPutLine(builder, "  9: 1001 = 9");
					AbstractProcessor.HelpPutLine(builder, "10: 1010 = A");
					AbstractProcessor.HelpPutLine(builder, "11: 1011 = B");
					AbstractProcessor.HelpPutLine(builder, "12: 1100 = C");
					AbstractProcessor.HelpPutLine(builder, "13: 1101 = D");
					AbstractProcessor.HelpPutLine(builder, "14: 1110 = E");
					AbstractProcessor.HelpPutLine(builder, "15: 1111 = F");

					AbstractProcessor.HelpPutTitle(builder, "Espace d'adressage");
					AbstractProcessor.HelpPutLine(builder, "[000]..[7FF] :<tab/>RAM");
					AbstractProcessor.HelpPutLine(builder, "[800]..[BFF] :<tab/>ROM");
					AbstractProcessor.HelpPutLine(builder, "[C00]..[C10] :<tab/>P�riph�riques");
					AbstractProcessor.HelpPutLine(builder, "[C80]..[CDF] :<tab/>Ecran bitmap");

					AbstractProcessor.HelpPutTitle(builder, "Affichage");
					AbstractProcessor.HelpPutLine(builder, "L'affichage est constitu� de 4 afficheurs � 7 segments (plus un point d�cimal), num�rot�s de droite � gauche. On peut �crire une valeur pour m�moriser les digits � allumer, ou relire cette valeur.");
					AbstractProcessor.HelpPutLine(builder, "[C00] :<tab/>Premier digit (celui de gauche).");
					AbstractProcessor.HelpPutLine(builder, "[C01] :<tab/>Deuxi�me digit.");
					AbstractProcessor.HelpPutLine(builder, "[C02] :<tab/>Troisi�me digit.");
					AbstractProcessor.HelpPutLine(builder, "[C03] :<tab/>Quatri�me digit (celui de droite).");
					AbstractProcessor.HelpPutLine(builder, "");
					AbstractProcessor.HelpPutLine(builder, "bit 0<tab/>Segment vertical sup�rieur droite.");
					AbstractProcessor.HelpPutLine(builder, "bit 1<tab/>Segment vertical inf�rieur droite.");
					AbstractProcessor.HelpPutLine(builder, "bit 2<tab/>Segment horizontal inf�rieur.");
					AbstractProcessor.HelpPutLine(builder, "bit 3<tab/>Segment vertical inf�rieur gauche.");
					AbstractProcessor.HelpPutLine(builder, "bit 4<tab/>Segment vertical sup�rieur gauche.");
					AbstractProcessor.HelpPutLine(builder, "bit 5<tab/>Segment horizontal sup�rieur.");
					AbstractProcessor.HelpPutLine(builder, "bit 6<tab/>Segment horizontal du milieu.");
					AbstractProcessor.HelpPutLine(builder, "bit 7<tab/>Point d�cimal.");

					AbstractProcessor.HelpPutTitle(builder, "Clavier");
					AbstractProcessor.HelpPutLine(builder, "Le clavier est constitu� de 8 touches nomm�es 0..7, plus 2 touches super-shift.");
					AbstractProcessor.HelpPutLine(builder, "[C07] :<tab/>Clavier.");
					AbstractProcessor.HelpPutLine(builder, "");
					AbstractProcessor.HelpPutLine(builder, "bits 0..2<tab/>Touches 0..7.");
					AbstractProcessor.HelpPutLine(builder, "bit 3<tab/>Touche Shift.");
					AbstractProcessor.HelpPutLine(builder, "bit 4<tab/>Touche Ctrl.");
					AbstractProcessor.HelpPutLine(builder, "bit 7<tab/>Prend la valeur 1 lorsqu'une touche 0..7 est press�e. Est automatiquement remis � z�ro lorsque l'adresse [C07] est lue.");

					AbstractProcessor.HelpPutTitle(builder, "Ecran bitmap");
					AbstractProcessor.HelpPutLine(builder, "L'�cran bitmap est un �cran vid�o monochrome de 32 x 24 pixels. Chaque byte repr�sente 8 pixels horizontaux, avec le bit 7 � gauche.");
					AbstractProcessor.HelpPutLine(builder, "");
					AbstractProcessor.HelpPutLine(builder, "[C80]..[C83] :<tab/>1�re ligne de 32 pixels.");
					AbstractProcessor.HelpPutLine(builder, "[C84]..[C87] :<tab/>2�me ligne de 32 pixels.");
					AbstractProcessor.HelpPutLine(builder, "...");
					AbstractProcessor.HelpPutLine(builder, "[CDC]..[CDF] :<tab/>24�me ligne de 32 pixels.");
					break;

				case "Data":
					AbstractProcessor.HelpPutTitle(builder, "Valeur imm�diate");
					break;

				case "Op":
					AbstractProcessor.HelpPutTitle(builder, "Op�rations arithm�tiques");
					break;

				case "Branch":
					AbstractProcessor.HelpPutTitle(builder, "Sauts");
					break;

				case "ROM":
					AbstractProcessor.HelpPutTitle(builder, "ROM");
					break;
			}

			return builder.ToString();
		}
		#endregion


		protected static readonly int FlagCarry    = 0;
		protected static readonly int FlagZero     = 1;
		protected static readonly int FlagNeg      = 2;
		protected static readonly int FlagOverflow = 3;

		
		protected int registerPC;  // program counter
		protected int registerSP;  // stack pointer
		protected int registerF;   // flags
		protected int registerA;   // accumulateur 8 bits
		protected int registerB;   // accumulateur 8 bits
		protected int registerX;   // pointeur 8 bits
		protected int registerY;   // pointeur 8 bits
	}
}
