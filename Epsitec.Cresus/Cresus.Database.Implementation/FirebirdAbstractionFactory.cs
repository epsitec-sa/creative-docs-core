ÿþ/ / 	 C o p y r i g h t   ©   2 0 0 3 - 2 0 0 8 ,   E P S I T E C   S A ,   1 4 0 0   Y v e r d o n - l e s - B a i n s ,   S w i t z e r l a n d  
 / / 	 A u t h o r :   P i e r r e   A R N A U D ,   M a i n t a i n e r :   P i e r r e   A R N A U D  
  
 u s i n g   E p s i t e c . C r e s u s . D a t a b a s e ;  
  
 u s i n g   F i r e b i r d S q l . D a t a . F i r e b i r d C l i e n t ;  
  
 n a m e s p a c e   E p s i t e c . C r e s u s . D a t a b a s e . I m p l e m e n t a t i o n  
 {  
 	 / / /   < s u m m a r y >  
 	 / / /   T h e   < c > F i r e b i r d A b s t r a c t i o n F a c t o r y < / c >   c l a s s   i m p l e m e n t s   t h e   < c > I D b A b s t r a c t i o n F a c t o r y < / c >  
 	 / / /   i n t e r f a c e   f o r   t h e   F i r e b i r d   e n g i n e .  
 	 / / /   < / s u m m a r y >  
 	 i n t e r n a l   s e a l e d   c l a s s   F i r e b i r d A b s t r a c t i o n F a c t o r y   :   I D b A b s t r a c t i o n F a c t o r y  
 	 {  
 	 	 p u b l i c   F i r e b i r d A b s t r a c t i o n F a c t o r y ( )  
 	 	 {  
 	 	 	 D b F a c t o r y . R e g i s t e r D a t a b a s e A b s t r a c t i o n   ( t h i s ) ;  
 	 	 }  
 	 	  
 	 	 # r e g i o n   I D b A b s t r a c t i o n F a c t o r y   M e m b e r s  
  
 	 	 p u b l i c   I D b A b s t r a c t i o n   C r e a t e D a t a b a s e A b s t r a c t i o n ( D b A c c e s s   d b A c c e s s )  
 	 	 {  
 	 	 	 S y s t e m . D i a g n o s t i c s . D e b u g . A s s e r t   ( d b A c c e s s . P r o v i d e r   = =   t h i s . P r o v i d e r N a m e ) ;  
  
 	 	 	 F i r e b i r d A b s t r a c t i o n   f b   =   n e w   F i r e b i r d A b s t r a c t i o n   ( d b A c c e s s ,   t h i s ,   E n g i n e T y p e . S e r v e r ) ;  
 	 	 	  
 	 	 	 r e t u r n   f b ;  
 	 	 }  
  
 	 	 p u b l i c   s t r i n g 	 	 	 	 	 	 	 P r o v i d e r N a m e  
 	 	 {  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   " F i r e b i r d " ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p u b l i c   I T y p e C o n v e r t e r 	 	 	 	 	 T y p e C o n v e r t e r  
 	 	 {  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . t y p e C o n v e r t e r ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p u b l i c   s t r i n g [ ]   Q u e r y D a t a b a s e F i l e P a t h s ( D b A c c e s s   d b A c c e s s )  
 	 	 {  
 	 	 	 r e t u r n   n e w   s t r i n g [ ]  
 	 	 	 {  
 	 	 	 	 F i r e b i r d A b s t r a c t i o n . M a k e D b F i l e P a t h   ( d b A c c e s s )  
 	 	 	 } ;  
 	 	 }  
  
 	 	 # e n d r e g i o n  
 	 	  
 	 	  
 	 	 p r i v a t e   F i r e b i r d T y p e C o n v e r t e r 	 	 	 t y p e C o n v e r t e r   =   n e w   F i r e b i r d T y p e C o n v e r t e r   ( ) ;  
 	 }  
  
  
 }  
 