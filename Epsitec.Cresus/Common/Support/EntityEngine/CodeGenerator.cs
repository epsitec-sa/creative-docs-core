ÿþ/ / 	 C o p y r i g h t   ©   2 0 0 7 - 2 0 1 2 ,   E P S I T E C   S A ,   1 4 0 0   Y v e r d o n - l e s - B a i n s ,   S w i t z e r l a n d  
 / / 	 A u t h o r :   P i e r r e   A R N A U D ,   M a i n t a i n e r :   P i e r r e   A R N A U D  
  
 u s i n g   E p s i t e c . C o m m o n . S u p p o r t ;  
 u s i n g   E p s i t e c . C o m m o n . S u p p o r t . C o d e G e n e r a t i o n ;  
 u s i n g   E p s i t e c . C o m m o n . S u p p o r t . E x t e n s i o n s ;  
 u s i n g   E p s i t e c . C o m m o n . T y p e s ;  
  
 u s i n g   S y s t e m . C o l l e c t i o n s . G e n e r i c ;  
 u s i n g   S y s t e m . L i n q ;  
 u s i n g   S y s t e m . C o l l e c t i o n s ;  
  
 n a m e s p a c e   E p s i t e c . C o m m o n . S u p p o r t . E n t i t y E n g i n e  
 {  
 	 u s i n g   K e y w o r d s = C o d e H e l p e r . K e y w o r d s ;  
  
 	 / / /   < s u m m a r y >  
 	 / / /   T h e   < c > C o d e G e n e r a t o r < / c >   c l a s s   p r o d u c e s   a   C #   s o u r c e   c o d e   w r a p p e r   f o r  
 	 / / /   t h e   e n t i t i e s   d e f i n e d   i n   t h e   r e s o u r c e   f i l e s .  
 	 / / /   < / s u m m a r y >  
 	 p u b l i c   c l a s s   C o d e G e n e r a t o r  
 	 {  
 	 	 / / /   < s u m m a r y >  
 	 	 / / /   I n i t i a l i z e s   a   n e w   i n s t a n c e   o f   t h e   < s e e   c r e f = " C o d e G e n e r a t o r " / >   c l a s s .  
 	 	 / / /   < / s u m m a r y >  
 	 	 / / /   < p a r a m   n a m e = " r e s o u r c e M a n a g e r " > T h e   r e s o u r c e   m a n a g e r . < / p a r a m >  
 	 	 p u b l i c   C o d e G e n e r a t o r ( R e s o u r c e M a n a g e r   r e s o u r c e M a n a g e r )  
 	 	 	 :   t h i s   ( n e w   C o d e F o r m a t t e r   ( ) ,   r e s o u r c e M a n a g e r )  
 	 	 {  
 	 	 }  
  
 	 	 / / /   < s u m m a r y >  
 	 	 / / /   I n i t i a l i z e s   a   n e w   i n s t a n c e   o f   t h e   < s e e   c r e f = " C o d e G e n e r a t o r " / >   c l a s s .  
 	 	 / / /   < / s u m m a r y >  
 	 	 / / /   < p a r a m   n a m e = " f o r m a t t e r " > T h e   f o r m a t t e r . < / p a r a m >  
 	 	 / / /   < p a r a m   n a m e = " r e s o u r c e M a n a g e r " > T h e   r e s o u r c e   m a n a g e r . < / p a r a m >  
 	 	 p u b l i c   C o d e G e n e r a t o r ( C o d e F o r m a t t e r   f o r m a t t e r ,   R e s o u r c e M a n a g e r   r e s o u r c e M a n a g e r )  
 	 	 {  
 	 	 	 t h i s . f o r m a t t e r   =   f o r m a t t e r ;  
 	 	 	 t h i s . r e s o u r c e M a n a g e r   =   r e s o u r c e M a n a g e r ;  
 	 	 	 t h i s . r e s o u r c e M a n a g e r P o o l   =   t h i s . r e s o u r c e M a n a g e r . P o o l ;  
 	 	 	 t h i s . r e s o u r c e M o d u l e I n f o   =   t h i s . r e s o u r c e M a n a g e r P o o l . G e t M o d u l e I n f o   ( t h i s . r e s o u r c e M a n a g e r . D e f a u l t M o d u l e P a t h ) ;  
 	 	 	 t h i s . s o u r c e N a m e s p a c e R e s   =   t h i s . r e s o u r c e M o d u l e I n f o . S o u r c e N a m e s p a c e R e s ;  
 	 	 	 t h i s . s o u r c e N a m e s p a c e E n t i t i e s   =   t h i s . r e s o u r c e M o d u l e I n f o . S o u r c e N a m e s p a c e E n t i t i e s ;  
 	 	 }  
  
  
 	 	 / / /   < s u m m a r y >  
 	 	 / / /   G e t s   t h e   f o r m a t t e r   u s e d   w h e n   g e n e r a t i n g   c o d e .  
 	 	 / / /   < / s u m m a r y >  
 	 	 / / /   < v a l u e > T h e   f o r m a t t e r . < / v a l u e >  
 	 	 p u b l i c   C o d e F o r m a t t e r   F o r m a t t e r  
 	 	 {  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . f o r m a t t e r ;  
 	 	 	 }  
 	 	 }  
  
 	 	 / / /   < s u m m a r y >  
 	 	 / / /   G e t s   t h e   r e s o u r c e   m a n a g e r .  
 	 	 / / /   < / s u m m a r y >  
 	 	 / / /   < v a l u e > T h e   r e s o u r c e   m a n a g e r . < / v a l u e >  
 	 	 p u b l i c   R e s o u r c e M a n a g e r   R e s o u r c e M a n a g e r  
 	 	 {  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . r e s o u r c e M a n a g e r ;  
 	 	 	 }  
 	 	 }  
  
 	 	 / / /   < s u m m a r y >  
 	 	 / / /   G e t s   t h e   r e s o u r c e   m a n a g e r   p o o l .  
 	 	 / / /   < / s u m m a r y >  
 	 	 / / /   < v a l u e > T h e   r e s o u r c e   m a n a g e r   p o o l . < / v a l u e >  
 	 	 p u b l i c   R e s o u r c e M a n a g e r P o o l   R e s o u r c e M a n a g e r P o o l  
 	 	 {  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . r e s o u r c e M a n a g e r P o o l ;  
 	 	 	 }  
 	 	 }  
  
 	 	 / / /   < s u m m a r y >  
 	 	 / / /   G e t s   t h e   r e s o u r c e   m o d u l e   i n f o r m a t i o n .  
 	 	 / / /   < / s u m m a r y >  
 	 	 / / /   < v a l u e > T h e   r e s o u r c e   m o d u l e   i n f o r m a t i o n . < / v a l u e >  
 	 	 p u b l i c   R e s o u r c e M o d u l e I n f o   R e s o u r c e M o d u l e I n f o  
 	 	 {  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . r e s o u r c e M o d u l e I n f o ;  
 	 	 	 }  
 	 	 }  
  
 	 	 / / /   < s u m m a r y >  
 	 	 / / /   G e t s   t h e   s o u r c e   n a m e s p a c e .  
 	 	 / / /   < / s u m m a r y >  
 	 	 / / /   < v a l u e > T h e   s o u r c e   n a m e s p a c e . < / v a l u e >  
 	 	 p u b l i c   s t r i n g   S o u r c e N a m e s p a c e R e s  
 	 	 {  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . s o u r c e N a m e s p a c e R e s   ? ?   t h i s . s o u r c e N a m e s p a c e E n t i t i e s ;  
 	 	 	 }  
 	 	 }  
  
 	 	 / / /   < s u m m a r y >  
 	 	 / / /   G e t s   t h e   s o u r c e   n a m e s p a c e .  
 	 	 / / /   < / s u m m a r y >  
 	 	 / / /   < v a l u e > T h e   s o u r c e   n a m e s p a c e . < / v a l u e >  
 	 	 p u b l i c   s t r i n g   S o u r c e N a m e s p a c e E n t i t i e s  
 	 	 {  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . s o u r c e N a m e s p a c e E n t i t i e s   ? ?   t h i s . s o u r c e N a m e s p a c e R e s ;  
 	 	 	 }  
 	 	 }  
  
  
 	 	 / / /   < s u m m a r y >  
 	 	 / / /   E m i t s   t h e   c o d e   f o r   a l l   e n t i t i e s   a n d   i n t e r f a c e s   d e f i n e d   i n   t h e  
 	 	 / / /   c u r r e n t   m o d u l e .  
 	 	 / / /   < / s u m m a r y >  
 	 	 p u b l i c   v o i d   E m i t ( )  
 	 	 {  
 	 	 	 C o d e H e l p e r . E m i t H e a d e r   ( t h i s . f o r m a t t e r ) ;  
  
 	 	 	 R e s o u r c e A c c e s s o r s . S t r u c t u r e d T y p e R e s o u r c e A c c e s s o r   a c c e s s o r   =   n e w   R e s o u r c e A c c e s s o r s . S t r u c t u r e d T y p e R e s o u r c e A c c e s s o r   ( ) ;  
 	 	 	 a c c e s s o r . L o a d   ( t h i s . r e s o u r c e M a n a g e r ) ;  
  
 	 	 	 f o r e a c h   ( C u l t u r e M a p   i t e m   i n   a c c e s s o r . C o l l e c t i o n )  
 	 	 	 {  
 	 	 	 	 C a p t i o n           c a p t i o n   =   t h i s . r e s o u r c e M a n a g e r . G e t C a p t i o n   ( i t e m . I d ) ;  
 	 	 	 	 S t r u c t u r e d T y p e   t y p e   =   T y p e R o s e t t a . C r e a t e T y p e O b j e c t   ( c a p t i o n ,   f a l s e )   a s   S t r u c t u r e d T y p e ;  
  
 	 	 	 	 i f   ( ( t y p e   ! =   n u l l )   & &  
 	 	 	 	 	 ( ( t y p e . C l a s s   = =   S t r u c t u r e d T y p e C l a s s . E n t i t y )   | |   ( t y p e . C l a s s   = =   S t r u c t u r e d T y p e C l a s s . I n t e r f a c e ) ) )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . E m i t   ( t y p e ) ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 t h i s . F o r m a t t e r . F l u s h   ( ) ;  
 	 	 }  
  
 	 	 / / /   < s u m m a r y >  
 	 	 / / /   E m i t s   t h e   s o u r c e   c o d e   f o r   t h e   e n t i t y   d e s c r i b e d   b y   t h e   s p e c i f i e d   t y p e .  
 	 	 / / /   < / s u m m a r y >  
 	 	 / / /   < p a r a m   n a m e = " t y p e " > T h e   s t r u c t u r e d   t y p e . < / p a r a m >  
 	 	 p u b l i c   v o i d   E m i t ( S t r u c t u r e d T y p e   t y p e )  
 	 	 {  
 	 	 	 S t r u c t u r e d T y p e C l a s s   t y p e C l a s s   =   t y p e . C l a s s ;  
 	 	 	  
 	 	 	 s t r i n g   n a m e                           =   t y p e . C a p t i o n . N a m e ;  
 	 	 	 v a r         s p e c i f i e r s               =   n e w   L i s t < s t r i n g >   ( ) ;  
 	 	 	 v a r         b a s e I n t e r f a c e I d s   =   n e w   H a s h S e t < D r u i d >   ( C o d e G e n e r a t o r . G e t F l a t I n t e r f a c e I d s   ( t y p e . B a s e T y p e ) ) ;  
  
 	 	 	 S y s t e m . D i a g n o s t i c s . D e b u g . A s s e r t   ( ( t y p e C l a s s   = =   S t r u c t u r e d T y p e C l a s s . E n t i t y )   | |   ( t y p e C l a s s   = =   S t r u c t u r e d T y p e C l a s s . I n t e r f a c e ) ) ;  
  
 	 	 	 / / 	 D e f i n e   t h e   b a s e   t y p e   f r o m   w h i c h   t h e   e n t i t y   c l a s s   w i l l   i n h e r i t ;   i t   i s  
 	 	 	 / / 	 e i t h e r   t h e   d e f i n e d   b a s e   t y p e   e n t i t y   o r   t h e   r o o t   A b s t r a c t E n t i t y   c l a s s  
 	 	 	 / / 	 f o r   e n t i t i e s .   F o r   i n t e r f a c e s ,   t h i s   d o e s   n o t   a p p l y .  
  
 	 	 	 s w i t c h   ( t y p e C l a s s )  
 	 	 	 {  
 	 	 	 	 c a s e   S t r u c t u r e d T y p e C l a s s . E n t i t y :  
 	 	 	 	 	 i f   ( t y p e . B a s e T y p e I d . I s V a l i d )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 s p e c i f i e r s . A d d   ( t h i s . C r e a t e E n t i t y F u l l N a m e   ( t y p e . B a s e T y p e I d ) ) ;  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e  
 	 	 	 	 	 {  
 	 	 	 	 	 	 s p e c i f i e r s . A d d   ( s t r i n g . C o n c a t   ( K e y w o r d s . G l o b a l ,   " : : " ,   K e y w o r d s . A b s t r a c t E n t i t y ) ) ;  
 	 	 	 	 	 }  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   S t r u c t u r e d T y p e C l a s s . I n t e r f a c e :  
 	 	 	 	 	 S y s t e m . D i a g n o s t i c s . D e b u g . A s s e r t   ( t y p e . B a s e T y p e I d . I s E m p t y ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 d e f a u l t :  
 	 	 	 	 	 t h r o w   n e w   S y s t e m . A r g u m e n t E x c e p t i o n   ( s t r i n g . F o r m a t   ( " S t r u c t u r e d T y p e C l a s s . { 0 }   n o t   v a l i d   i n   t h i s   c o n t e x t " ,   t y p e C l a s s ) ) ;  
 	 	 	 }  
  
 	 	 	 / / 	 I n c l u d e   a l l   l o c a l l y   i m p o r t e d   i n t e r f a c e s ,   i f   a n y   :  
  
 	 	 	 f o r e a c h   ( D r u i d   i n t e r f a c e I d   i n   t y p e . I n t e r f a c e I d s . W h e r e   ( i d   = >   ! b a s e I n t e r f a c e I d s . C o n t a i n s   ( i d ) ) )  
 	 	 	 {  
 	 	 	 	 s p e c i f i e r s . A d d   ( t h i s . C r e a t e E n t i t y F u l l N a m e   ( i n t e r f a c e I d ) ) ;  
 	 	 	 }  
  
 	 	 	 E m i t t e r   e m i t t e r ;  
 	 	 	 I n t e r f a c e I m p l e m e n t a t i o n E m i t t e r   i m p l e m e n t a t i o n E m i t t e r ;  
  
 	 	 	 t h i s . f o r m a t t e r . W r i t e C o d e L i n e   ( K e y w o r d s . B e g i n R e g i o n ,   "   " ,   t h i s . S o u r c e N a m e s p a c e E n t i t i e s ,   " . " ,   n a m e ,   "   " ,   t y p e C l a s s . T o S t r i n g   ( ) ) ;  
  
 	 	 	 i f   ( t y p e C l a s s   = =   S t r u c t u r e d T y p e C l a s s . E n t i t y )  
 	 	 	 {  
 	 	 	 	 t h i s . f o r m a t t e r . W r i t e A s s e m b l y A t t r i b u t e   ( " [ a s s e m b l y :   " ,   K e y w o r d s . E n t i t y C l a s s A t t r i b u t e ,   @ "   ( " " " ,   t y p e . C a p t i o n I d . T o S t r i n g   ( ) ,   @ " " " ,   t y p e o f   ( " ,   C o d e G e n e r a t o r . C r e a t e E n t i t y N a m e s p a c e   ( t h i s . S o u r c e N a m e s p a c e E n t i t i e s ) ,   " . " ,   C o d e G e n e r a t o r . C r e a t e E n t i t y I d e n t i f i e r   ( n a m e ) ,   " ) ) ] " ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . f o r m a t t e r . W r i t e B e g i n N a m e s p a c e   ( C o d e G e n e r a t o r . C r e a t e E n t i t y N a m e s p a c e   ( t h i s . S o u r c e N a m e s p a c e E n t i t i e s ) ) ;  
  
 	 	 	 t h i s . E m i t C l a s s C o m m e n t   ( t y p e ) ;  
  
 	 	 	 s w i t c h   ( t y p e C l a s s )  
 	 	 	 {  
 	 	 	 	 c a s e   S t r u c t u r e d T y p e C l a s s . E n t i t y :  
 	 	 	 	 	 e m i t t e r   =   n e w   E n t i t y E m i t t e r   ( t h i s ,   t y p e ,   b a s e I n t e r f a c e I d s ) ;  
  
 	 	 	 	 	 C o d e A t t r i b u t e s   c l a s s A t t r i b u t e s   =  
 	 	 	 	 	 	 t y p e . F l a g s . H a s F l a g   ( S t r u c t u r e d T y p e F l a g s . A b s t r a c t C l a s s )   & &   ! t y p e . F l a g s . H a s F l a g   ( S t r u c t u r e d T y p e F l a g s . G e n e r a t e R e p o s i t o r y )  
 	 	 	 	 	 	 ?   C o d e H e l p e r . A b s t r a c t E n t i t y C l a s s A t t r i b u t e s   :   C o d e H e l p e r . E n t i t y C l a s s A t t r i b u t e s ;  
 	 	 	 	 	  
 	 	 	 	 	 t h i s . f o r m a t t e r . W r i t e B e g i n C l a s s   ( c l a s s A t t r i b u t e s ,   C o d e G e n e r a t o r . C r e a t e E n t i t y I d e n t i f i e r   ( n a m e ) ,   s p e c i f i e r s ) ;  
 	 	 	 	 	 t y p e . F o r E a c h F i e l d   ( e m i t t e r . E m i t L o c a l P r o p e r t y ) ;  
 	 	 	 	 	 e m i t t e r . E m i t C l o s e R e g i o n   ( ) ;  
 	 	 	 	 	 t h i s . f o r m a t t e r . W r i t e C o d e L i n e   ( ) ;  
 	 	 	 	 	 t y p e . F o r E a c h F i e l d   ( e m i t t e r . E m i t L o c a l P r o p e r t y H a n d l e r s ) ;  
 	 	 	 	 	 e m i t t e r . E m i t C l o s e R e g i o n   ( ) ;  
 	 	 	 	 	 t h i s . f o r m a t t e r . W r i t e C o d e L i n e   ( ) ;  
 	 	 	 	 	 t y p e . F o r E a c h F i e l d   ( e m i t t e r . E m i t M e t h o d s F o r L o c a l V i r t u a l P r o p e r t i e s ) ;  
 	 	 	 	 	 t h i s . f o r m a t t e r . W r i t e C o d e L i n e   ( ) ;  
 	 	 	 	 	 t h i s . E m i t M e t h o d s   ( t y p e ) ;  
 	 	 	 	 	 e m i t t e r . E m i t C l a s s e s   ( t y p e ) ;  
 	 	 	 	 	 t h i s . f o r m a t t e r . W r i t e E n d C l a s s   ( ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   S t r u c t u r e d T y p e C l a s s . I n t e r f a c e :  
 	 	 	 	 	 e m i t t e r   =   n e w   I n t e r f a c e E m i t t e r   ( t h i s ,   t y p e ) ;  
  
 	 	 	 	 	 t h i s . f o r m a t t e r . W r i t e B e g i n I n t e r f a c e   ( C o d e H e l p e r . P u b l i c I n t e r f a c e A t t r i b u t e s ,   C o d e G e n e r a t o r . C r e a t e I n t e r f a c e I d e n t i f i e r   ( n a m e ) ,   s p e c i f i e r s ) ;  
 	 	 	 	 	 t y p e . F o r E a c h F i e l d   ( e m i t t e r . E m i t L o c a l P r o p e r t y ) ;  
 	 	 	 	 	 e m i t t e r . E m i t C l o s e R e g i o n   ( ) ;  
 	 	 	 	 	 t h i s . f o r m a t t e r . W r i t e E n d I n t e r f a c e   ( ) ;  
  
 	 	 	 	 	 i m p l e m e n t a t i o n E m i t t e r   =   n e w   I n t e r f a c e I m p l e m e n t a t i o n E m i t t e r   ( t h i s ,   t y p e ) ;  
  
 	 	 	 	 	 t h i s . f o r m a t t e r . W r i t e B e g i n C l a s s   ( C o d e H e l p e r . P u b l i c S t a t i c P a r t i a l C l a s s A t t r i b u t e s ,   C o d e G e n e r a t o r . C r e a t e I n t e r f a c e I m p l e m e n t a t i o n I d e n t i f i e r   ( n a m e ) ) ;  
 	 	 	 	 	 t y p e . F o r E a c h F i e l d   ( i m p l e m e n t a t i o n E m i t t e r . E m i t L o c a l P r o p e r t y I m p l e m e n t a t i o n ) ;  
 	 	 	 	 	 t h i s . f o r m a t t e r . W r i t e E n d C l a s s   ( ) ;  
 	 	 	 	 	 b r e a k ;  
 	 	 	 }  
  
 	 	 	 t h i s . f o r m a t t e r . W r i t e E n d N a m e s p a c e   ( ) ;  
 	 	 	 t h i s . f o r m a t t e r . W r i t e C o d e L i n e   ( K e y w o r d s . E n d R e g i o n ) ;  
 	 	 	 t h i s . f o r m a t t e r . W r i t e C o d e L i n e   ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   s t a t i c   I E n u m e r a b l e < D r u i d >   G e t F l a t I n t e r f a c e I d s ( S t r u c t u r e d T y p e   t y p e )  
 	 	 {  
 	 	 	 w h i l e   ( t y p e   ! =   n u l l )  
 	 	 	 {  
 	 	 	 	 f o r e a c h   ( v a r   i d   i n   t y p e . I n t e r f a c e I d s )  
 	 	 	 	 {  
 	 	 	 	 	 y i e l d   r e t u r n   i d ;  
 	 	 	 	 }  
  
 	 	 	 	 t y p e   =   t y p e . B a s e T y p e ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   E m i t C l a s s C o m m e n t ( S t r u c t u r e d T y p e   t y p e )  
 	 	 {  
 	 	 	 s t r i n g   d e s c r i p t i o n   =   t y p e . C a p t i o n . D e s c r i p t i o n ;  
  
 	 	 	 i f   ( s t r i n g . I s N u l l O r E m p t y   ( d e s c r i p t i o n ) )  
 	 	 	 {  
 	 	 	 	 d e s c r i p t i o n   =   s t r i n g . C o n c a t   ( " T h e   < c > " ,   t y p e . C a p t i o n . N a m e ,   " < / c >   e n t i t y . " ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . f o r m a t t e r . W r i t e C o d e L i n e   ( K e y w o r d s . X m l C o m m e n t ,   " \ t " ,   K e y w o r d s . X m l B e g i n S u m m a r y ) ;  
 	 	 	  
 	 	 	 f o r e a c h   ( s t r i n g   l i n e   i n   d e s c r i p t i o n . S p l i t   ( n e w   c h a r [ ]   {   ' \ n ' ,   ' \ r '   } ,   S y s t e m . S t r i n g S p l i t O p t i o n s . R e m o v e E m p t y E n t r i e s ) )  
 	 	 	 {  
 	 	 	 	 t h i s . f o r m a t t e r . W r i t e C o d e L i n e   ( K e y w o r d s . X m l C o m m e n t ,   " \ t " ,   l i n e ) ;  
 	 	 	 }  
 	 	 	  
 	 	 	 t h i s . f o r m a t t e r . W r i t e C o d e L i n e   ( K e y w o r d s . X m l C o m m e n t ,   " \ t " ,   K e y w o r d s . D e s i g n e r C a p t i o n P r o t o c o l ,   t y p e . C a p t i o n I d . T o S t r i n g   ( ) . T r i m   ( ' [ ' ,   ' ] ' ) ) ;  
 	 	 	 t h i s . f o r m a t t e r . W r i t e C o d e L i n e   ( K e y w o r d s . X m l C o m m e n t ,   " \ t " ,   K e y w o r d s . X m l E n d S u m m a r y ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   E m i t P r o p e r t y C o m m e n t ( S t r u c t u r e d T y p e   t y p e ,   S t r u c t u r e d T y p e F i e l d   f i e l d ,   s t r i n g   f i e l d N a m e )  
 	 	 {  
 	 	 	 C a p t i o n   c a p t i o n   =   t h i s . r e s o u r c e M a n a g e r . G e t C a p t i o n   ( f i e l d . C a p t i o n I d ) ;  
 	 	 	 s t r i n g   d e s c r i p t i o n   =   c a p t i o n   = =   n u l l   ?   n u l l   :   c a p t i o n . D e s c r i p t i o n ;  
  
 	 	 	 i f   ( s t r i n g . I s N u l l O r E m p t y   ( d e s c r i p t i o n ) )  
 	 	 	 {  
 	 	 	 	 d e s c r i p t i o n   =   s t r i n g . C o n c a t   ( " T h e   < c > " ,   f i e l d N a m e ,   " < / c >   f i e l d . " ) ;  
 	 	 	 }  
 	 	 	  
 	 	 	 t h i s . f o r m a t t e r . W r i t e C o d e L i n e   ( K e y w o r d s . X m l C o m m e n t ,   " \ t " ,   K e y w o r d s . X m l B e g i n S u m m a r y ) ;  
 	 	 	  
 	 	 	 f o r e a c h   ( s t r i n g   l i n e   i n   d e s c r i p t i o n . S p l i t   ( n e w   c h a r [ ]   {   ' \ n ' ,   ' \ r '   } ,   S y s t e m . S t r i n g S p l i t O p t i o n s . R e m o v e E m p t y E n t r i e s ) )  
 	 	 	 {  
 	 	 	 	 t h i s . f o r m a t t e r . W r i t e C o d e L i n e   ( K e y w o r d s . X m l C o m m e n t ,   " \ t " ,   l i n e ) ;  
 	 	 	 }  
 	 	 	  
 	 	 	 t h i s . f o r m a t t e r . W r i t e C o d e L i n e   ( K e y w o r d s . X m l C o m m e n t ,   " \ t " ,   K e y w o r d s . D e s i g n e r E n t i t y F i e l d P r o t o c o l ,   t y p e . C a p t i o n I d . T o S t r i n g   ( ) . T r i m   ( ' [ ' ,   ' ] ' ) ,   " / " ,   f i e l d . I d . T r i m   ( ' [ ' ,   ' ] ' ) ) ;  
 	 	 	 t h i s . f o r m a t t e r . W r i t e C o d e L i n e   ( K e y w o r d s . X m l C o m m e n t ,   " \ t " ,   K e y w o r d s . X m l E n d S u m m a r y ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   E m i t P r o p e r t y A t t r i b u t e ( S t r u c t u r e d T y p e   s t r u c t u r e d T y p e ,   S t r u c t u r e d T y p e F i e l d   f i e l d ,   s t r i n g   p r o p N a m e )  
 	 	 {  
 	 	 	 i f   ( f i e l d . O p t i o n s . H a s F l a g   ( F i e l d O p t i o n s . V i r t u a l ) )  
 	 	 	 {  
 	 	 	 	 t h i s . f o r m a t t e r . W r i t e C o d e L i n e   ( " [ " ,   K e y w o r d s . E n t i t y F i e l d A t t r i b u t e ,   "   ( " ,   @ " " " " ,   f i e l d . C a p t i o n I d . T o S t r i n g   ( ) ,   @ " " " " ,   " ,   " ,   K e y w o r d s . E n t i t y F i e l d A t t r i b u t e I s V i r t u a l ,   " ) ] " ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . f o r m a t t e r . W r i t e C o d e L i n e   ( " [ " ,   K e y w o r d s . E n t i t y F i e l d A t t r i b u t e ,   "   ( " ,   @ " " " " ,   f i e l d . C a p t i o n I d . T o S t r i n g   ( ) ,   @ " " " " ,   " ) ] " ) ;  
 	 	 	 }  
 	 	 }  
 	 	  
 	 	 p r i v a t e   v o i d   E m i t M e t h o d s ( S t r u c t u r e d T y p e   t y p e )  
 	 	 {  
 	 	 	 D r u i d   i d   =   t y p e . C a p t i o n I d ;  
 	 	 	 s t r i n g   c l a s s N a m e   =   t h i s . C r e a t e E n t i t y F u l l N a m e   ( t y p e . C a p t i o n I d ) ;  
  
 	 	 	 s t r i n g   c o d e ;  
 	 	 	  
 	 	 	 c o d e   =   s t r i n g . C o n c a t   ( K e y w o r d s . D r u i d ,   "   " ,   K e y w o r d s . G e t E n t i t y S t r u c t u r e d T y p e I d M e t h o d ,   " ( ) " ) ;  
 	 	 	 t h i s . f o r m a t t e r . W r i t e B e g i n M e t h o d   ( C o d e H e l p e r . P u b l i c O v e r r i d e M e t h o d A t t r i b u t e s ,   c o d e ) ;  
 	 	 	 t h i s . f o r m a t t e r . W r i t e C o d e L i n e   ( K e y w o r d s . R e t u r n ,   "   " ,   c l a s s N a m e ,   " . " ,   K e y w o r d s . E n t i t y S t r u c t u r e d T y p e I d P r o p e r t y ,   " ; " ) ;  
 	 	 	 t h i s . f o r m a t t e r . W r i t e E n d M e t h o d   ( ) ;  
  
 	 	 	 c o d e   =   s t r i n g . C o n c a t   ( K e y w o r d s . S t r i n g ,   "   " ,   K e y w o r d s . G e t E n t i t y S t r u c t u r e d T y p e K e y M e t h o d ,   " ( ) " ) ;  
 	 	 	 t h i s . f o r m a t t e r . W r i t e B e g i n M e t h o d   ( C o d e H e l p e r . P u b l i c O v e r r i d e M e t h o d A t t r i b u t e s ,   c o d e ) ;  
 	 	 	 t h i s . f o r m a t t e r . W r i t e C o d e L i n e   ( K e y w o r d s . R e t u r n ,   "   " ,   c l a s s N a m e ,   " . " ,   K e y w o r d s . E n t i t y S t r u c t u r e d T y p e K e y P r o p e r t y ,   " ; " ) ;  
 	 	 	 t h i s . f o r m a t t e r . W r i t e E n d M e t h o d   ( ) ;  
  
 	 	 	 C o d e A t t r i b u t e s   f i e l d A t t r i b u t e s   =   t y p e . B a s e T y p e   = =   n u l l  
 	 	 	 	 ?   C o d e H e l p e r . P u b l i c S t a t i c R e a d O n l y F i e l d A t t r i b u t e s  
 	 	 	 	 :   C o d e H e l p e r . P u b l i c S t a t i c N e w R e a d O n l y F i e l d A t t r i b u t e s ;  
  
 	 	 	 t h i s . f o r m a t t e r . W r i t e F i e l d   ( f i e l d A t t r i b u t e s ,   K e y w o r d s . D r u i d ,   "   " ,   K e y w o r d s . E n t i t y S t r u c t u r e d T y p e I d P r o p e r t y ,   "   =   " ,  
 	 	 	 	 / * * / 	 	 	 	       K e y w o r d s . N e w ,   "   " ,   K e y w o r d s . D r u i d ,   "   ( " ,  
 	 	 	 	 / * * / 	 	 	 	       i d . M o d u l e . T o S t r i n g   ( S y s t e m . G l o b a l i z a t i o n . C u l t u r e I n f o . I n v a r i a n t C u l t u r e ) ,   " ,   " ,  
 	 	 	 	 / * * / 	 	 	 	       i d . D e v e l o p e r A n d P a t c h L e v e l . T o S t r i n g   ( S y s t e m . G l o b a l i z a t i o n . C u l t u r e I n f o . I n v a r i a n t C u l t u r e ) ,   " ,   " ,  
 	 	 	 	 / * * / 	 	 	 	       i d . L o c a l . T o S t r i n g   ( S y s t e m . G l o b a l i z a t i o n . C u l t u r e I n f o . I n v a r i a n t C u l t u r e ) ,   " ) ; " ,  
 	 	 	 	 / * * / 	 	 	 	       " \ t " ,   K e y w o r d s . S i m p l e C o m m e n t ,   "   " ,   i d . T o S t r i n g   ( ) ) ;  
  
 	 	 	 t h i s . f o r m a t t e r . W r i t e F i e l d   ( f i e l d A t t r i b u t e s ,   K e y w o r d s . S t r i n g ,   "   " ,   K e y w o r d s . E n t i t y S t r u c t u r e d T y p e K e y P r o p e r t y ,   "   =   " ,  
 	 	 	 	 / * * / 	 	 	 	       K e y w o r d s . Q u o t e ,   i d . T o S t r i n g   ( ) ,   K e y w o r d s . Q u o t e ,   " ; " ) ;  
 	 	 }  
  
  
 	 	 p r i v a t e   s t a t i c   s t r i n g   C r e a t e E n t i t y I d e n t i f i e r ( s t r i n g   n a m e )  
 	 	 {  
 	 	 	 r e t u r n   s t r i n g . C o n c a t   ( n a m e ,   K e y w o r d s . E n t i t y S u f f i x ) ;  
 	 	 }  
  
 	 	 p r i v a t e   s t a t i c   s t r i n g   C r e a t e P r o p e r t y I d e n t i f i e r ( s t r i n g   n a m e )  
 	 	 {  
 	 	 	 r e t u r n   n a m e ;  
 	 	 }  
  
 	 	 p r i v a t e   s t a t i c   s t r i n g   C r e a t e M e t h o d I d e n t i f i e r F o r V i r t u a l P r o p e r t y G e t t e r ( s t r i n g   n a m e )  
 	 	 {  
 	 	 	 r e t u r n   s t r i n g . C o n c a t   ( " G e t " ,   n a m e ) ;  
 	 	 }  
  
 	 	 p r i v a t e   s t a t i c   s t r i n g   C r e a t e M e t h o d I d e n t i f i e r F o r V i r t u a l P r o p e r t y S e t t e r ( s t r i n g   n a m e )  
 	 	 {  
 	 	 	 r e t u r n   s t r i n g . C o n c a t   ( " S e t " ,   n a m e ) ;  
 	 	 }  
  
 	 	 p r i v a t e   s t a t i c   s t r i n g   C r e a t e I n t e r f a c e I d e n t i f i e r ( s t r i n g   n a m e )  
 	 	 {  
 	 	 	 r e t u r n   n a m e ;  
 	 	 }  
  
 	 	 p r i v a t e   s t a t i c   s t r i n g   C r e a t e I n t e r f a c e I m p l e m e n t a t i o n I d e n t i f i e r ( s t r i n g   n a m e )  
 	 	 {  
 	 	 	 r e t u r n   s t r i n g . C o n c a t   ( n a m e ,   K e y w o r d s . I n t e r f a c e I m p l e m e n t a t i o n S u f f i x ) ;  
 	 	 }  
  
 	 	 p r i v a t e   s t a t i c   s t r i n g   C r e a t e E n t i t y N a m e s p a c e ( s t r i n g   n a m e )  
 	 	 {  
 	 	 	 r e t u r n   s t r i n g . C o n c a t   ( n a m e ,   " . " ,   K e y w o r d s . E n t i t i e s ) ;  
 	 	 }  
  
 	 	 p r i v a t e   s t r i n g   C r e a t e E n t i t y S h o r t N a m e ( D r u i d   i d )  
 	 	 {  
 	 	 	 C a p t i o n   c a p t i o n   =   t h i s . r e s o u r c e M a n a g e r . G e t C a p t i o n   ( i d ) ;  
 	 	 	 S t r u c t u r e d T y p e   t y p e   =   T y p e R o s e t t a . G e t T y p e O b j e c t   ( c a p t i o n )   a s   S t r u c t u r e d T y p e ;  
  
 	 	 	 S y s t e m . D i a g n o s t i c s . D e b u g . A s s e r t   ( t y p e   ! =   n u l l ) ;  
  
 	 	 	 s t r i n g   i d e n t i f i e r ;  
  
 	 	 	 s w i t c h   ( t y p e . C l a s s )  
 	 	 	 {  
 	 	 	 	 c a s e   S t r u c t u r e d T y p e C l a s s . E n t i t y :  
 	 	 	 	 	 i d e n t i f i e r   =   C o d e G e n e r a t o r . C r e a t e E n t i t y I d e n t i f i e r   ( c a p t i o n . N a m e ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   S t r u c t u r e d T y p e C l a s s . I n t e r f a c e :  
 	 	 	 	 	 i d e n t i f i e r   =   C o d e G e n e r a t o r . C r e a t e I n t e r f a c e I d e n t i f i e r   ( c a p t i o n . N a m e ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 d e f a u l t :  
 	 	 	 	 	 t h r o w   n e w   S y s t e m . A r g u m e n t E x c e p t i o n   ( s t r i n g . F o r m a t   ( " S t r u c t u r e d T y p e C l a s s . { 0 }   n o t   v a l i d   i n   t h i s   c o n t e x t " ,   t y p e . C l a s s ) ) ;  
 	 	 	 }  
  
 	 	 	 r e t u r n   i d e n t i f i e r ;  
 	 	 }  
 	 	  
 	 	 p r i v a t e   s t r i n g   C r e a t e E n t i t y F u l l N a m e ( D r u i d   i d )  
 	 	 {  
 	 	 	 C a p t i o n   c a p t i o n   =   t h i s . r e s o u r c e M a n a g e r . G e t C a p t i o n   ( i d ) ;  
 	 	 	 S t r u c t u r e d T y p e   t y p e   =   T y p e R o s e t t a . G e t T y p e O b j e c t   ( c a p t i o n )   a s   S t r u c t u r e d T y p e ;  
  
 	 	 	 I L i s t < R e s o u r c e M o d u l e I n f o >   i n f o s   =   t h i s . r e s o u r c e M a n a g e r P o o l . F i n d M o d u l e I n f o s   ( i d . M o d u l e ) ;  
 	 	 	  
 	 	 	 S y s t e m . D i a g n o s t i c s . D e b u g . A s s e r t   ( i n f o s . C o u n t   >   0 ) ;  
 	 	 	 S y s t e m . D i a g n o s t i c s . D e b u g . A s s e r t   ( c a p t i o n   ! =   n u l l ) ;  
 	 	 	 S y s t e m . D i a g n o s t i c s . D e b u g . A s s e r t   ( ! s t r i n g . I s N u l l O r E m p t y   ( i n f o s [ 0 ] . S o u r c e N a m e s p a c e E n t i t i e s ) ) ;  
 	 	 	 S y s t e m . D i a g n o s t i c s . D e b u g . A s s e r t   ( t y p e   ! =   n u l l ) ;  
 	 	 	 S y s t e m . D i a g n o s t i c s . D e b u g . A s s e r t   ( ! s t r i n g . I s N u l l O r E m p t y   ( c a p t i o n . N a m e ) ) ;  
  
 	 	 	 s t r i n g   i d e n t i f i e r ;  
  
 	 	 	 s w i t c h   ( t y p e . C l a s s )  
 	 	 	 {  
 	 	 	 	 c a s e   S t r u c t u r e d T y p e C l a s s . E n t i t y :  
 	 	 	 	 	 i d e n t i f i e r   =   C o d e G e n e r a t o r . C r e a t e E n t i t y I d e n t i f i e r   ( c a p t i o n . N a m e ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   S t r u c t u r e d T y p e C l a s s . I n t e r f a c e :  
 	 	 	 	 	 i d e n t i f i e r   =   C o d e G e n e r a t o r . C r e a t e I n t e r f a c e I d e n t i f i e r   ( c a p t i o n . N a m e ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 d e f a u l t :  
 	 	 	 	 	 t h r o w   n e w   S y s t e m . A r g u m e n t E x c e p t i o n   ( s t r i n g . F o r m a t   ( " S t r u c t u r e d T y p e C l a s s . { 0 }   n o t   v a l i d   i n   t h i s   c o n t e x t " ,   t y p e . C l a s s ) ) ;  
 	 	 	 }  
  
 	 	 	 r e t u r n   s t r i n g . C o n c a t   ( K e y w o r d s . G l o b a l ,   " : : " ,   C o d e G e n e r a t o r . C r e a t e E n t i t y N a m e s p a c e   ( i n f o s [ 0 ] . S o u r c e N a m e s p a c e E n t i t i e s ) ,   " . " ,   i d e n t i f i e r ) ;  
 	 	 }  
  
 	 	 p r i v a t e   s t r i n g   C r e a t e P r o p e r t y N a m e ( D r u i d   i d )  
 	 	 {  
 	 	 	 C a p t i o n   c a p t i o n   =   t h i s . r e s o u r c e M a n a g e r . G e t C a p t i o n   ( i d ) ;  
 	 	 	 r e t u r n   C o d e G e n e r a t o r . C r e a t e P r o p e r t y I d e n t i f i e r   ( c a p t i o n . N a m e ) ;  
 	 	 }  
  
 	 	 p r i v a t e   s t r i n g   C r e a t e M e t h o d N a m e F o r V i r t u a l P r o p e r t y G e t t e r ( D r u i d   i d )  
 	 	 {  
 	 	 	 C a p t i o n   c a p t i o n   =   t h i s . r e s o u r c e M a n a g e r . G e t C a p t i o n   ( i d ) ;  
 	 	 	 r e t u r n   C o d e G e n e r a t o r . C r e a t e M e t h o d I d e n t i f i e r F o r V i r t u a l P r o p e r t y G e t t e r   ( c a p t i o n . N a m e ) ;  
 	 	 }  
  
 	 	 p r i v a t e   s t r i n g   C r e a t e M e t h o d N a m e F o r V i r t u a l P r o p e r t y S e t t e r ( D r u i d   i d )  
 	 	 {  
 	 	 	 C a p t i o n   c a p t i o n   =   t h i s . r e s o u r c e M a n a g e r . G e t C a p t i o n   ( i d ) ;  
 	 	 	 r e t u r n   C o d e G e n e r a t o r . C r e a t e M e t h o d I d e n t i f i e r F o r V i r t u a l P r o p e r t y S e t t e r   ( c a p t i o n . N a m e ) ;  
 	 	 }  
  
 	 	 p r i v a t e   s t r i n g   C r e a t e T y p e F u l l N a m e ( D r u i d   t y p e I d )  
 	 	 {  
 	 	 	 r e t u r n   t h i s . C r e a t e T y p e F u l l N a m e   ( t y p e I d ,   f a l s e ) ;  
 	 	 }  
 	 	  
 	 	 p r i v a t e   s t r i n g   C r e a t e T y p e F u l l N a m e ( D r u i d   t y p e I d ,   b o o l   n u l l a b l e )  
 	 	 {  
 	 	 	 C a p t i o n   c a p t i o n   =   t h i s . r e s o u r c e M a n a g e r . G e t C a p t i o n   ( t y p e I d ) ;  
 	 	 	 A b s t r a c t T y p e   t y p e   =   T y p e R o s e t t a . G e t T y p e O b j e c t   ( c a p t i o n ) ;  
 	 	 	 S y s t e m . T y p e   s y s T y p e   =   t y p e . S y s t e m T y p e ;  
  
 	 	 	 i f   ( ( s y s T y p e   = =   t y p e o f   ( S t r u c t u r e d T y p e ) )   | |  
 	 	 	 	 ( s y s T y p e   = =   n u l l ) )  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . C r e a t e E n t i t y F u l l N a m e   ( t y p e I d ) ;  
 	 	 	 }  
 	 	 	 e l s e   i f   ( n u l l a b l e   & &   ( s y s T y p e . I s V a l u e T y p e )   & &   ( ! T y p e R o s e t t a . I s N u l l a b l e   ( s y s T y p e ) ) )  
 	 	 	 {  
 	 	 	 	 r e t u r n   s t r i n g . C o n c a t   ( C o d e G e n e r a t o r . G e t T y p e N a m e   ( s y s T y p e ) ,   " ? " ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 r e t u r n   C o d e G e n e r a t o r . G e t T y p e N a m e   ( s y s T y p e ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   s t r i n g   C r e a t e F i e l d T y p e F u l l N a m e ( S t r u c t u r e d T y p e F i e l d   f i e l d )  
 	 	 {  
 	 	 	 s t r i n g   t y p e N a m e   =   t h i s . C r e a t e T y p e F u l l N a m e   ( f i e l d . T y p e I d ,   T y p e R o s e t t a . I s N u l l a b l e   ( f i e l d ) ) ;  
  
 	 	 	 i f   ( f i e l d . R e l a t i o n   = =   F i e l d R e l a t i o n . C o l l e c t i o n )  
 	 	 	 {  
 	 	 	 	 t y p e N a m e   =   s t r i n g . C o n c a t   ( K e y w o r d s . G l o b a l ,   " : : " ,   K e y w o r d s . G e n e r i c I L i s t ,   " < " ,   t y p e N a m e ,   " > " ) ;  
 	 	 	 }  
  
 	 	 	 r e t u r n   t y p e N a m e ;  
 	 	 }  
  
 	 	 p r i v a t e   s t a t i c   s t r i n g   G e t T y p e N a m e ( S y s t e m . T y p e   s y s t e m T y p e )  
 	 	 {  
 	 	 	 s t r i n g   s y s t e m T y p e N a m e   =   s y s t e m T y p e . F u l l N a m e ;  
  
 	 	 	 s w i t c h   ( s y s t e m T y p e N a m e )  
 	 	 	 {  
 	 	 	 	 c a s e   " S y s t e m . B o o l e a n " :  
 	 	 	 	 	 r e t u r n   " b o o l " ;  
 	 	 	 	 c a s e   " S y s t e m . I n t 1 6 " :  
 	 	 	 	 	 r e t u r n   " s h o r t " ;  
 	 	 	 	 c a s e   " S y s t e m . I n t 3 2 " :  
 	 	 	 	 	 r e t u r n   " i n t " ;  
 	 	 	 	 c a s e   " S y s t e m . I n t 6 4 " :  
 	 	 	 	 	 r e t u r n   " l o n g " ;  
 	 	 	 	 c a s e   " S y s t e m . S t r i n g " :  
 	 	 	 	 	 r e t u r n   " s t r i n g " ;  
 	 	 	 }  
  
 	 	 	 r e t u r n   s t r i n g . C o n c a t   ( K e y w o r d s . G l o b a l ,   " : : " ,   s y s t e m T y p e N a m e ) ;  
 	 	 }  
  
 	 	 # r e g i o n   E m i t t e r   C l a s s  
  
 	 	 p r i v a t e   a b s t r a c t   c l a s s   E m i t t e r  
 	 	 {  
 	 	 	 p u b l i c   E m i t t e r ( C o d e G e n e r a t o r   g e n e r a t o r ,   S t r u c t u r e d T y p e   t y p e )  
 	 	 	 {  
 	 	 	 	 t h i s . g e n e r a t o r   =   g e n e r a t o r ;  
 	 	 	 	 t h i s . t y p e   =   t y p e ;  
 	 	 	 }  
  
 	 	 	 p u b l i c   v i r t u a l   v o i d   E m i t L o c a l P r o p e r t y ( S t r u c t u r e d T y p e F i e l d   f i e l d )  
 	 	 	 {  
 	 	 	 	 i f   ( ( f i e l d . M e m b e r s h i p   = =   F i e l d M e m b e r s h i p . L o c a l )   | |  
 	 	 	 	 	 ( f i e l d . M e m b e r s h i p   = =   F i e l d M e m b e r s h i p . L o c a l O v e r r i d e ) )  
 	 	 	 	 {  
 	 	 	 	 	 i f   ( t h i s . S u p p r e s s F i e l d   ( f i e l d ) )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 r e t u r n ;  
 	 	 	 	 	 }  
  
 	 	 	 	 	 t h i s . E m i t I n t e r f a c e R e g i o n   ( f i e l d ) ;  
  
 	 	 	 	 	 s t r i n g   t y p e N a m e   =   t h i s . g e n e r a t o r . C r e a t e T y p e F u l l N a m e   ( f i e l d . T y p e I d ,   T y p e R o s e t t a . I s N u l l a b l e   ( f i e l d ) ) ;  
 	 	 	 	 	 s t r i n g   p r o p N a m e   =   t h i s . g e n e r a t o r . C r e a t e P r o p e r t y N a m e   ( f i e l d . C a p t i o n I d ) ;  
  
 	 	 	 	 	 t h i s . g e n e r a t o r . E m i t P r o p e r t y C o m m e n t   ( t h i s . t y p e ,   f i e l d ,   p r o p N a m e ) ;  
 	 	 	 	 	 t h i s . g e n e r a t o r . E m i t P r o p e r t y A t t r i b u t e   ( t h i s . t y p e ,   f i e l d ,   p r o p N a m e ) ;  
 	 	 	 	 	  
 	 	 	 	 	 t h i s . E m i t L o c a l B e g i n P r o p e r t y   ( f i e l d ,   t y p e N a m e ,   p r o p N a m e ) ;  
  
 	 	 	 	 	 s w i t c h   ( f i e l d . S o u r c e )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 c a s e   F i e l d S o u r c e . V a l u e :  
 	 	 	 	 	 	 	 t h i s . E m i t P r o p e r t y G e t t e r   ( f i e l d ,   t y p e N a m e ,   p r o p N a m e ) ;  
 	 	 	 	 	 	 	 t h i s . E m i t P r o p e r t y S e t t e r   ( f i e l d ,   t y p e N a m e ,   p r o p N a m e ) ;  
 	 	 	 	 	 	 	 b r e a k ;  
 	 	 	 	 	 	  
 	 	 	 	 	 	 c a s e   F i e l d S o u r c e . E x p r e s s i o n :  
 	 	 	 	 	 	 	 t h i s . E m i t P r o p e r t y G e t t e r   ( f i e l d ,   t y p e N a m e ,   p r o p N a m e ) ;  
 	 	 	 	 	 	 	 t h i s . E m i t P r o p e r t y S e t t e r   ( f i e l d ,   t y p e N a m e ,   p r o p N a m e ) ;  
 	 	 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 	 	 d e f a u l t :  
 	 	 	 	 	 	 	 t h r o w   n e w   S y s t e m . A r g u m e n t E x c e p t i o n   ( s t r i n g . F o r m a t   ( " F i e l d S o u r c e . { 0 }   n o t   v a l i d   i n   t h i s   c o n t e x t " ,   f i e l d . S o u r c e ) ) ;  
 	 	 	 	 	 }  
  
 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e E n d P r o p e r t y   ( ) ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 p r o t e c t e d   v i r t u a l   b o o l   S u p p r e s s F i e l d ( S t r u c t u r e d T y p e F i e l d   f i e l d )  
 	 	 	 {  
 	 	 	 	 r e t u r n   f a l s e ;  
 	 	 	 }  
  
 	 	 	 p u b l i c   v o i d   E m i t L o c a l P r o p e r t y H a n d l e r s ( S t r u c t u r e d T y p e F i e l d   f i e l d )  
 	 	 	 {  
 	 	 	 	 i f   ( ( f i e l d . M e m b e r s h i p   = =   F i e l d M e m b e r s h i p . L o c a l )   & &  
 	 	 	 	 	 ( f i e l d . D e f i n i n g T y p e I d . I s E m p t y ) )  
 	 	 	 	 {  
 	 	 	 	 	 s t r i n g   t y p e N a m e   =   t h i s . g e n e r a t o r . C r e a t e T y p e F u l l N a m e   ( f i e l d . T y p e I d ,   T y p e R o s e t t a . I s N u l l a b l e   ( f i e l d ) ) ;  
 	 	 	 	 	 s t r i n g   p r o p N a m e   =   t h i s . g e n e r a t o r . C r e a t e P r o p e r t y N a m e   ( f i e l d . C a p t i o n I d ) ;  
  
 	 	 	 	 	 s w i t c h   ( f i e l d . R e l a t i o n )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 c a s e   F i e l d R e l a t i o n . N o n e :  
 	 	 	 	 	 	 c a s e   F i e l d R e l a t i o n . R e f e r e n c e :  
 	 	 	 	 	 	 	 t h i s . E m i t P r o p e r t y O n C h a n g i n g   ( f i e l d ,   t y p e N a m e ,   p r o p N a m e ) ;  
 	 	 	 	 	 	 	 t h i s . E m i t P r o p e r t y O n C h a n g e d   ( f i e l d ,   t y p e N a m e ,   p r o p N a m e ) ;  
 	 	 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 	 	 c a s e   F i e l d R e l a t i o n . C o l l e c t i o n :  
 	 	 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 	 	 d e f a u l t :  
 	 	 	 	 	 	 	 t h r o w   n e w   S y s t e m . N o t S u p p o r t e d E x c e p t i o n   ( s t r i n g . F o r m a t   ( " R e l a t i o n   { 0 }   n o t   s u p p o r t e d " ,   f i e l d . R e l a t i o n ) ) ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 p u b l i c   v o i d   E m i t M e t h o d s F o r L o c a l V i r t u a l P r o p e r t i e s ( S t r u c t u r e d T y p e F i e l d   f i e l d )  
 	 	 	 {  
 	 	 	 	 i f   ( ( f i e l d . M e m b e r s h i p   = =   F i e l d M e m b e r s h i p . L o c a l )   & &  
 	 	 	 	 	 ( f i e l d . D e f i n i n g T y p e I d . I s E m p t y ) )  
 	 	 	 	 {  
 	 	 	 	 	 i f   ( f i e l d . O p t i o n s . H a s F l a g   ( F i e l d O p t i o n s . V i r t u a l ) )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 s t r i n g   f i e l d T y p e N a m e   =   t h i s . g e n e r a t o r . C r e a t e F i e l d T y p e F u l l N a m e   ( f i e l d ) ;  
  
 	 	 	 	 	 	 s w i t c h   ( f i e l d . R e l a t i o n )  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 c a s e   F i e l d R e l a t i o n . N o n e :  
 	 	 	 	 	 	 	 c a s e   F i e l d R e l a t i o n . R e f e r e n c e :  
 	 	 	 	 	 	 	 	 t h i s . E m i t M e t h o d F o r V i r t u a l P r o p e r t y G e t t e r   ( f i e l d ,   f i e l d T y p e N a m e ) ;  
 	 	 	 	 	 	 	 	 t h i s . E m i t M e t h o d F o r V i r t u a l P r o p e r t y S e t t e r   ( f i e l d ,   f i e l d T y p e N a m e ) ;  
 	 	 	 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 	 	 	 c a s e   F i e l d R e l a t i o n . C o l l e c t i o n :  
 	 	 	 	 	 	 	 	 t h i s . E m i t M e t h o d F o r V i r t u a l P r o p e r t y G e t t e r   ( f i e l d ,   f i e l d T y p e N a m e ) ;  
 	 	 	 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 	 	 	 d e f a u l t :  
 	 	 	 	 	 	 	 	 t h r o w   n e w   S y s t e m . N o t S u p p o r t e d E x c e p t i o n   ( s t r i n g . F o r m a t   ( " R e l a t i o n   { 0 }   n o t   s u p p o r t e d " ,   f i e l d . R e l a t i o n ) ) ;  
 	 	 	 	 	 	 }  
 	 	 	 	 	 }  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 p u b l i c   v o i d   E m i t C l o s e R e g i o n ( )  
 	 	 	 {  
 	 	 	 	 t h i s . E m i t I n t e r f a c e R e g i o n   ( n u l l ) ;  
 	 	 	 }  
  
 	 	 	 p r o t e c t e d   v i r t u a l   v o i d   E m i t P r o p e r t y O n C h a n g i n g ( S t r u c t u r e d T y p e F i e l d   f i e l d ,   s t r i n g   t y p e N a m e ,   s t r i n g   p r o p N a m e )  
 	 	 	 {  
 	 	 	 }  
  
 	 	 	 p r o t e c t e d   v i r t u a l   v o i d   E m i t P r o p e r t y O n C h a n g e d ( S t r u c t u r e d T y p e F i e l d   f i e l d ,   s t r i n g   t y p e N a m e ,   s t r i n g   p r o p N a m e )  
 	 	 	 {  
 	 	 	 }  
  
 	 	 	 p r o t e c t e d   v i r t u a l   v o i d   E m i t P r o p e r t y G e t t e r ( S t r u c t u r e d T y p e F i e l d   f i e l d ,   s t r i n g   t y p e N a m e ,   s t r i n g   p r o p N a m e )  
 	 	 	 {  
 	 	 	 }  
  
 	 	 	 p r o t e c t e d   v i r t u a l   v o i d   E m i t P r o p e r t y S e t t e r ( S t r u c t u r e d T y p e F i e l d   f i e l d ,   s t r i n g   t y p e N a m e ,   s t r i n g   p r o p N a m e )  
 	 	 	 {  
 	 	 	 }  
  
 	 	 	 p r o t e c t e d   v i r t u a l   v o i d   E m i t M e t h o d F o r V i r t u a l P r o p e r t y G e t t e r ( S t r u c t u r e d T y p e F i e l d   f i e l d ,   s t r i n g   t y p e N a m e )  
 	 	 	 {  
 	 	 	 }  
  
 	 	 	 p r o t e c t e d   v i r t u a l   v o i d   E m i t M e t h o d F o r V i r t u a l P r o p e r t y S e t t e r ( S t r u c t u r e d T y p e F i e l d   f i e l d ,   s t r i n g   t y p e N a m e )  
 	 	 	 {  
 	 	 	 }  
  
 	 	 	 p r i v a t e   v o i d   E m i t L o c a l B e g i n P r o p e r t y ( S t r u c t u r e d T y p e F i e l d   f i e l d ,   s t r i n g   t y p e N a m e ,   s t r i n g   p r o p N a m e )  
 	 	 	 {  
 	 	 	 	 s t r i n g   c o d e ;  
  
 	 	 	 	 s w i t c h   ( f i e l d . R e l a t i o n )  
 	 	 	 	 {  
 	 	 	 	 	 c a s e   F i e l d R e l a t i o n . N o n e :  
 	 	 	 	 	 c a s e   F i e l d R e l a t i o n . R e f e r e n c e :  
 	 	 	 	 	 	 c o d e   =   s t r i n g . C o n c a t   ( t y p e N a m e ,   "   " ,   p r o p N a m e ) ;  
 	 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 	 c a s e   F i e l d R e l a t i o n . C o l l e c t i o n :  
 	 	 	 	 	 	 c o d e   =   s t r i n g . C o n c a t   ( K e y w o r d s . G l o b a l ,   " : : " ,   K e y w o r d s . G e n e r i c I L i s t ,   " < " ,   t y p e N a m e ,   " > " ,   "   " ,   p r o p N a m e ) ;  
 	 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 	 d e f a u l t :  
 	 	 	 	 	 	 t h r o w   n e w   S y s t e m . A r g u m e n t E x c e p t i o n   ( s t r i n g . F o r m a t   ( " F i e l d R e l a t i o n . { 0 }   n o t   v a l i d   i n   t h i s   c o n t e x t " ,   f i e l d . R e l a t i o n ) ) ;  
 	 	 	 	 }  
  
 	 	 	 	 i f   ( f i e l d . S o u r c e   = =   F i e l d S o u r c e . E x p r e s s i o n )  
 	 	 	 	 {  
 	 	 	 	 	 D r u i d   d e f i n i n g T y p e I d   =   f i e l d . D e f i n i n g T y p e I d ;  
  
 	 	 	 	 	 i f   ( ( d e f i n i n g T y p e I d . I s E m p t y )   | |  
 	 	 	 	 	 	 ( t h i s . t y p e . I n t e r f a c e I d s . C o n t a i n s   ( d e f i n i n g T y p e I d ) ) )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e B e g i n P r o p e r t y   ( C o d e H e l p e r . P u b l i c V i r t u a l P r o p e r t y A t t r i b u t e s ,   c o d e ) ;  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e  
 	 	 	 	 	 {  
 	 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e B e g i n P r o p e r t y   ( C o d e H e l p e r . P u b l i c O v e r r i d e P r o p e r t y A t t r i b u t e s ,   c o d e ) ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e B e g i n P r o p e r t y   ( C o d e H e l p e r . P u b l i c P r o p e r t y A t t r i b u t e s ,   c o d e ) ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 p r i v a t e   v o i d   E m i t I n t e r f a c e R e g i o n ( S t r u c t u r e d T y p e F i e l d   f i e l d )  
 	 	 	 {  
 	 	 	 	 D r u i d   c u r r e n t I n t e r f a c e I d   =   f i e l d   = =   n u l l   ?   D r u i d . E m p t y   :   f i e l d . D e f i n i n g T y p e I d ;  
  
 	 	 	 	 i f   ( t h i s . i n t e r f a c e I d   = =   c u r r e n t I n t e r f a c e I d )  
 	 	 	 	 {  
 	 	 	 	 	 r e t u r n ;  
 	 	 	 	 }  
  
 	 	 	 	 i f   ( t h i s . i n t e r f a c e I d . I s V a l i d )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e C o d e L i n e   ( K e y w o r d s . E n d R e g i o n ) ;  
 	 	 	 	 }  
  
 	 	 	 	 t h i s . i n t e r f a c e I d   =   c u r r e n t I n t e r f a c e I d ;  
  
 	 	 	 	 i f   ( t h i s . i n t e r f a c e I d . I s V a l i d )  
 	 	 	 	 {  
 	 	 	 	 	 s t r i n g   i n t e r f a c e N a m e   =   t h i s . g e n e r a t o r . C r e a t e E n t i t y S h o r t N a m e   ( t h i s . i n t e r f a c e I d ) ;  
 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e C o d e L i n e   ( K e y w o r d s . B e g i n R e g i o n ,   "   " ,   i n t e r f a c e N a m e ,   "   M e m b e r s " ) ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 p u b l i c   v o i d   E m i t C l a s s e s ( S t r u c t u r e d T y p e   t y p e )  
 	 	 	 {  
 	 	 	 	 i f   ( t y p e . F l a g s . H a s F l a g   ( S t r u c t u r e d T y p e F l a g s . G e n e r a t e R e p o s i t o r y ) )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . E m i t R e p o s i t o r y C l a s s   ( t y p e ) ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 p r i v a t e   v o i d   E m i t R e p o s i t o r y C l a s s ( S t r u c t u r e d T y p e   t y p e )  
 	 	 	 {  
 	 	 	 	 s t r i n g   e n t i t y C l a s s N a m e   =   C o d e G e n e r a t o r . C r e a t e E n t i t y I d e n t i f i e r   ( t y p e . C a p t i o n . N a m e ) ;  
 	 	 	 	 s t r i n g   e x p e c t a n c y   =   t y p e . D e f a u l t L i f e t i m e E x p e c t a n c y . T o S t r i n g   ( ) ;  
 	 	 	 	  
 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e C o d e L i n e   ( ) ;  
  
 	 	 	 	 t h i s . E m i t C l a s s R e g i o n   ( K e y w o r d s . R e p o s i t o r y ) ;  
  
 	 	 	 	 b o o l   o v e r r i d e s B a s e R e p o s i t o r y   =   t h i s . I s B a s e R e p o s i t o r y O v e r r i d e n   ( t y p e ) ;  
  
 	 	 	 	 C o d e A t t r i b u t e s   r e p o s i t o r y A t t r i b u t e s   =   o v e r r i d e s B a s e R e p o s i t o r y  
 	 	 	 	 	 ?   C o d e H e l p e r . R e p o s i t o r y N e w C l a s s A t t r i b u t e s  
 	 	 	 	 	 :   C o d e H e l p e r . R e p o s i t o r y C l a s s A t t r i b u t e s ;  
  
 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e B e g i n C l a s s   ( r e p o s i t o r y A t t r i b u t e s ,   K e y w o r d s . R e p o s i t o r y ,  
 	 	 	 	 	 s t r i n g . C o n c a t   ( K e y w o r d s . Q u a l i f i e d G e n e r i c R e p o s i t o r y B a s e ,   " < " ,   e n t i t y C l a s s N a m e ,   " > " ) ) ;  
  
 	 	 	 	 s t r i n g   c o d e   =   s t r i n g . C o n c a t   ( K e y w o r d s . R e p o s i t o r y ,   " ( " ,  
 	 	 	 	 	 / * * / 	 	 	 	 	   K e y w o r d s . Q u a l i f i e d C o r e D a t a ,   "   " ,   K e y w o r d s . D a t a V a r i a b l e ,   " ,   " ,  
 	 	 	 	 	 / * * / 	 	 	 	           K e y w o r d s . Q u a l i f i e d D a t a C o n t e x t ,   "   " ,   K e y w o r d s . D a t a C o n t e x t V a r i a b l e ,  
 	 	 	 	 	 / * * / 	 	 	 	 	   " ) " ,   "   :   " ,   K e y w o r d s . B a s e ,   " ( " ,  
 	 	 	 	 	 / * * / 	 	 	 	 	   K e y w o r d s . D a t a V a r i a b l e ,   " ,   " ,   K e y w o r d s . D a t a C o n t e x t V a r i a b l e ,   " ,   " ,  
 	 	 	 	 	 / * * / 	 	 	 	 	   K e y w o r d s . Q u a l i f i e d D a t a L i f e t i m e ,   " . " ,   e x p e c t a n c y ,   " ) " ) ;  
 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e B e g i n M e t h o d   ( C o d e H e l p e r . P u b l i c A t t r i b u t e s ,   c o d e ) ;  
 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e E n d M e t h o d   ( ) ;  
 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e E n d C l a s s   ( ) ;  
  
 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e C o d e L i n e   ( K e y w o r d s . E n d R e g i o n ) ;  
 	 	 	 }  
  
 	 	 	 p r i v a t e   b o o l   I s B a s e R e p o s i t o r y O v e r r i d e n ( S t r u c t u r e d T y p e   t y p e )  
 	 	 	 {  
 	 	 	 	 b o o l   o v e r r i d e s B a s e R e p o s i t o r y   =   f a l s e ;  
  
 	 	 	 	 S t r u c t u r e d T y p e   b a s e T y p e   =   t y p e . B a s e T y p e ;  
  
 	 	 	 	 w h i l e   ( ! o v e r r i d e s B a s e R e p o s i t o r y   & &   b a s e T y p e   ! =   n u l l )  
 	 	 	 	 {  
 	 	 	 	 	 o v e r r i d e s B a s e R e p o s i t o r y   =   t y p e . B a s e T y p e . F l a g s . H a s F l a g   ( S t r u c t u r e d T y p e F l a g s . G e n e r a t e R e p o s i t o r y ) ;  
  
 	 	 	 	 	 b a s e T y p e   =   b a s e T y p e . B a s e T y p e ;  
 	 	 	 	 }  
  
 	 	 	 	 r e t u r n   o v e r r i d e s B a s e R e p o s i t o r y ;  
 	 	 	 }  
  
 	 	 	 p r o t e c t e d   v o i d   E m i t C l a s s R e g i o n ( s t r i n g   c l a s s N a m e )  
 	 	 	 {  
 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e C o d e L i n e   ( K e y w o r d s . B e g i n R e g i o n ,   "   " ,   c l a s s N a m e ,   "   C l a s s " ) ;  
 	 	 	 }  
  
 	 	 	 p r o t e c t e d   C o d e G e n e r a t o r   g e n e r a t o r ;  
 	 	 	 p r o t e c t e d   S t r u c t u r e d T y p e   t y p e ;  
  
 	 	 	 p r i v a t e   D r u i d   i n t e r f a c e I d ;  
 	 	 }  
  
 	 	 # e n d r e g i o n  
  
 	 	 # r e g i o n   E n t i t y E m i t t e r   C l a s s  
  
 	 	 p r i v a t e   s e a l e d   c l a s s   E n t i t y E m i t t e r   :   E m i t t e r  
 	 	 {  
 	 	 	 p u b l i c   E n t i t y E m i t t e r ( C o d e G e n e r a t o r   g e n e r a t o r ,   S t r u c t u r e d T y p e   t y p e ,   H a s h S e t < D r u i d >   b a s e E n t i t y I d s )  
 	 	 	 	 :   b a s e   ( g e n e r a t o r ,   t y p e )  
 	 	 	 {  
 	 	 	 	 t h i s . b a s e E n t i t y I d s   =   b a s e E n t i t y I d s ;  
 	 	 	 }  
  
 	 	 	 p r o t e c t e d   o v e r r i d e   b o o l   S u p p r e s s F i e l d ( S t r u c t u r e d T y p e F i e l d   f i e l d )  
 	 	 	 {  
 	 	 	 	 / / 	 D o n ' t   g e n e r a t e   f i e l d s   d e f i n e d   i n   i n h e r i t e d   i n t e r f a c e s .  
 	 	 	 	 i f   ( t h i s . b a s e E n t i t y I d s . C o n t a i n s   ( f i e l d . D e f i n i n g T y p e I d ) )  
 	 	 	 	 {  
 	 	 	 	 	 r e t u r n   t r u e ;  
 	 	 	 	 }  
  
 	 	 	 	 r e t u r n   f a l s e ;  
 	 	 	 }  
  
 	 	 	 p u b l i c   o v e r r i d e   v o i d   E m i t L o c a l P r o p e r t y ( S t r u c t u r e d T y p e F i e l d   f i e l d )  
 	 	 	 {  
 	 	 	 	 b a s e . E m i t L o c a l P r o p e r t y   ( f i e l d ) ;  
  
 	 	 	 	 i f   ( f i e l d . S o u r c e   = =   F i e l d S o u r c e . E x p r e s s i o n )  
 	 	 	 	 {  
 	 	 	 	 	 i f   ( ( f i e l d . D e f i n i n g T y p e I d . I s E m p t y )   | |  
 	 	 	 	 	 	 ( f i e l d . M e m b e r s h i p   = =   F i e l d M e m b e r s h i p . L o c a l O v e r r i d e ) )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 / / 	 T h e   f i e l d   i s   d e f i n e d   l o c a l l y   a n d   i s   n o t   t h e   i m p l e m e n t a t i o n   o f   a n y  
 	 	 	 	 	 	 / / 	 i n t e r f a c e .  
 	 	 	 	 	 	  
 	 	 	 	 	 	 s t r i n g   t y p e N a m e   =   t h i s . g e n e r a t o r . C r e a t e T y p e F u l l N a m e   ( f i e l d . T y p e I d ,   T y p e R o s e t t a . I s N u l l a b l e   ( f i e l d ) ) ;  
 	 	 	 	 	 	 s t r i n g   p r o p N a m e   =   t h i s . g e n e r a t o r . C r e a t e P r o p e r t y N a m e   ( f i e l d . C a p t i o n I d ) ;  
  
 	 	 	 	 	 	 t h i s . E m i t E x p r e s s i o n   ( f i e l d ,   t y p e N a m e ,   p r o p N a m e ) ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 p r o t e c t e d   o v e r r i d e   v o i d   E m i t P r o p e r t y G e t t e r ( S t r u c t u r e d T y p e F i e l d   f i e l d ,   s t r i n g   t y p e N a m e ,   s t r i n g   p r o p N a m e )  
 	 	 	 {  
 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e B e g i n G e t t e r   ( C o d e H e l p e r . P u b l i c A t t r i b u t e s ) ;  
  
 	 	 	 	 i f   ( ( f i e l d . D e f i n i n g T y p e I d . I s E m p t y )   | |  
 	 	 	 	 	 ( f i e l d . M e m b e r s h i p   = =   F i e l d M e m b e r s h i p . L o c a l O v e r r i d e ) )  
 	 	 	 	 {  
 	 	 	 	 	 / / 	 T h e   f i e l d   i s   d e f i n e d   l o c a l l y   a n d   i s   n o t   t h e   i m p l e m e n t a t i o n   o f   a n y  
 	 	 	 	 	 / / 	 i n t e r f a c e .  
  
 	 	 	 	 	 i f   ( f i e l d . O p t i o n s . H a s F l a g   ( F i e l d O p t i o n s . V i r t u a l ) )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 / /   T h e   f i e l d   i s   v i r t u a l   a n d   w e   m u s t   f e t c h   i t s   v a l u e   w i t h   a   p a r t i a l   m e t h o d .  
  
 	 	 	 	 	 	 v a r   m e t h o d N a m e   =   t h i s . g e n e r a t o r . C r e a t e M e t h o d N a m e F o r V i r t u a l P r o p e r t y G e t t e r   ( f i e l d . C a p t i o n I d ) ;  
 	 	 	 	 	 	 v a r   f i e l d T y p e N a m e   =   t h i s . g e n e r a t o r . C r e a t e F i e l d T y p e F u l l N a m e   ( f i e l d ) ;  
  
 	 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e C o d e L i n e   ( f i e l d T y p e N a m e ,   "   " ,   K e y w o r d s . V a l u e V a r i a b l e ,   "   =   " ,   K e y w o r d s . D e f a u l t ,   "   ( " ,   f i e l d T y p e N a m e ,   " ) " ,   " ; " ) ;  
 	 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e C o d e L i n e   ( K e y w o r d s . T h i s ,   " . " ,   m e t h o d N a m e ,   "   ( " ,   K e y w o r d s . R e f ,   "   " ,   K e y w o r d s . V a l u e V a r i a b l e ,   " ) " ,   " ; " ) ;  
 	 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e C o d e L i n e   ( K e y w o r d s . R e t u r n ,   "   " ,   K e y w o r d s . V a l u e V a r i a b l e ,   " ; " ) ;  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e  
 	 	 	 	 	 {  
 	 	 	 	 	 	 / /   T h e   f i e l d   i s   n o t   v i r t u a l   s o   w e   a p p l y   t h e   r e g u l a r   w a y   o f   f e t c h i n g   i t s  
 	 	 	 	 	 	 / /   v a l u e .  
  
 	 	 	 	 	 	 s w i t c h   ( f i e l d . S o u r c e )  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 c a s e   F i e l d S o u r c e . V a l u e :  
 	 	 	 	 	 	 	 	 t h i s . E m i t V a l u e G e t t e r   ( f i e l d ,   t y p e N a m e ) ;  
 	 	 	 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 	 	 	 c a s e   F i e l d S o u r c e . E x p r e s s i o n :  
 	 	 	 	 	 	 	 	 t h i s . E m i t E x p r e s s i o n G e t t e r   ( f i e l d ,   t y p e N a m e ,   p r o p N a m e ) ;  
 	 	 	 	 	 	 	 	 b r e a k ;  
 	 	 	 	 	 	 }  
 	 	 	 	 	 }  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 / / 	 T h e   f i e l d   i s   d e f i n e d   b y   a n   i n t e r f a c e   a n d   t h i s   i s   a n   i m p l e m e n t a t i o n  
 	 	 	 	 	 / / 	 o f   t h e   g e t t e r .  
  
 	 	 	 	 	 s t r i n g   i n t e r f a c e N a m e   =   t h i s . g e n e r a t o r . C r e a t e T y p e F u l l N a m e   ( f i e l d . D e f i n i n g T y p e I d ) ;  
 	 	 	 	 	 s t r i n g   i n t e r f a c e I m p l e m e n t a t i o n N a m e   =   C o d e G e n e r a t o r . C r e a t e I n t e r f a c e I m p l e m e n t a t i o n I d e n t i f i e r   ( i n t e r f a c e N a m e ) ;  
 	 	 	 	 	 s t r i n g   g e t t e r M e t h o d N a m e   =   s t r i n g . C o n c a t   ( K e y w o r d s . I n t e r f a c e I m p l e m e n t a t i o n G e t t e r M e t h o d P r e f i x ,   p r o p N a m e ) ;  
  
 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e C o d e L i n e   ( K e y w o r d s . R e t u r n ,   "   " ,   i n t e r f a c e I m p l e m e n t a t i o n N a m e ,   " . " ,   g e t t e r M e t h o d N a m e ,   "   ( " ,   K e y w o r d s . T h i s ,   " ) ; " ) ;  
 	 	 	 	 }  
 	 	 	 	  
 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e E n d G e t t e r   ( ) ;  
 	 	 	 }  
  
 	 	 	 p r i v a t e   v o i d   E m i t E x p r e s s i o n ( S t r u c t u r e d T y p e F i e l d   f i e l d ,   s t r i n g   t y p e N a m e ,   s t r i n g   p r o p N a m e )  
 	 	 	 {  
 	 	 	 	 E n t i t y E x p r e s s i o n   e x p r e s s i o n   =   E n t i t y E x p r e s s i o n . F r o m E n c o d e d E x p r e s s i o n   ( f i e l d . E x p r e s s i o n ) ;  
  
 	 	 	 	 i f   ( e x p r e s s i o n . E n c o d i n g   ! =   E n t i t y E x p r e s s i o n E n c o d i n g . L a m b d a C S h a r p S o u r c e C o d e )  
 	 	 	 	 {  
 	 	 	 	 	 t h r o w   n e w   S y s t e m . I n v a l i d O p e r a t i o n E x c e p t i o n   ( s t r i n g . F o r m a t   ( " I n v a l i d   e x p r e s s i o n   e n c o d i n g   ' { 0 } '   f o r   f i e l d   { 1 } . { 2 } " ,   e x p r e s s i o n . E n c o d i n g ,   t h i s . t y p e . N a m e ,   p r o p N a m e ) ) ;  
 	 	 	 	 }  
  
 	 	 	 	 s t r i n g   c l a s s N a m e   =   t h i s . g e n e r a t o r . C r e a t e E n t i t y F u l l N a m e   ( t h i s . t y p e . C a p t i o n I d ) ;  
 	 	 	 	 s t r i n g   c o m m e n t   =   s t r i n g . C o n c a t   ( "   " ,   K e y w o r d s . S i m p l e C o m m e n t ,   "   »  " ,   t h i s . t y p e . C a p t i o n I d . T o S t r i n g   ( ) ,   "   " ,   f i e l d . I d ) ;  
  
 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e F i e l d   ( C o d e H e l p e r . P r i v a t e S t a t i c R e a d O n l y F i e l d A t t r i b u t e s ,  
 	 	 	 	 	 K e y w o r d s . F u n c ,   " < " ,   c l a s s N a m e ,   " ,   " ,   t y p e N a m e ,   " >   " ,   K e y w o r d s . F u n c P r e f i x ,   p r o p N a m e ,   "   =   " ,  
 	 	 	 	 	 e x p r e s s i o n . S o u r c e C o d e ,   " ; " ,   c o m m e n t ) ;  
  
 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e F i e l d   ( C o d e H e l p e r . P r i v a t e S t a t i c R e a d O n l y F i e l d A t t r i b u t e s ,  
 	 	 	 	 	 K e y w o r d s . L i n q E x p r e s s i o n ,   " < " ,   K e y w o r d s . F u n c ,   " < " ,   c l a s s N a m e ,   " ,   " ,   t y p e N a m e ,   " > >   " ,   K e y w o r d s . E x p r P r e f i x ,   p r o p N a m e ,   "   =   " ,  
 	 	 	 	 	 e x p r e s s i o n . S o u r c e C o d e ,   " ; " ,   c o m m e n t ) ;  
 	 	 	 }  
  
 	 	 	 p r i v a t e   v o i d   E m i t V a l u e G e t t e r ( S t r u c t u r e d T y p e F i e l d   f i e l d ,   s t r i n g   t y p e N a m e )  
 	 	 	 {  
 	 	 	 	 i f   ( f i e l d . R e l a t i o n   = =   F i e l d R e l a t i o n . C o l l e c t i o n )  
 	 	 	 	 {  
 	 	 	 	 	 i f   ( f i e l d . T y p e   i s   S t r u c t u r e d T y p e )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e C o d e L i n e   ( K e y w o r d s . R e t u r n ,   "   " ,   K e y w o r d s . T h i s ,   " . " ,   K e y w o r d s . G e t F i e l d C o l l e c t i o n M e t h o d ,   " < " ,   t y p e N a m e ,   " >   ( " ,   K e y w o r d s . Q u o t e ,   f i e l d . I d ,   K e y w o r d s . Q u o t e ,   " ) ; " ) ;  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e  
 	 	 	 	 	 {  
 	 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e C o d e L i n e   ( K e y w o r d s . T h r o w ,   "   " ,   K e y w o r d s . N e w ,   "   " ,   K e y w o r d s . N o t S u p p o r t e d E x c e p t i o n ,   @ "   ( " " C o l l e c t i o n   o f   t y p e   " ,   t y p e N a m e ,   @ "   n o t   s u p p o r t e d " " ) ; " ) ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e C o d e L i n e   ( K e y w o r d s . R e t u r n ,   "   " ,   K e y w o r d s . T h i s ,   " . " ,   K e y w o r d s . G e t F i e l d M e t h o d ,   " < " ,   t y p e N a m e ,   " >   ( " ,   K e y w o r d s . Q u o t e ,   f i e l d . I d ,   K e y w o r d s . Q u o t e ,   " ) ; " ) ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 p r i v a t e   v o i d   E m i t E x p r e s s i o n G e t t e r ( S t r u c t u r e d T y p e F i e l d   f i e l d ,   s t r i n g   t y p e N a m e ,   s t r i n g   p r o p N a m e )  
 	 	 	 {  
 	 	 	 	 s t r i n g   c l a s s N a m e   =   t h i s . g e n e r a t o r . C r e a t e E n t i t y F u l l N a m e   ( t h i s . t y p e . C a p t i o n I d ) ;  
  
 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e C o d e L i n e   (  
 	 	 	 	 	 K e y w o r d s . R e t u r n ,   "   " ,  
 	 	 	 	 	 K e y w o r d s . G l o b a l ,   " : : " ,   K e y w o r d s . A b s t r a c t E n t i t y ,   " . " ,   K e y w o r d s . G e t C a l c u l a t i o n M e t h o d ,   " < " ,   c l a s s N a m e ,   " ,   " ,   t y p e N a m e ,   " >   ( " ,  
 	 	 	 	 	 K e y w o r d s . T h i s ,   " ,   " ,  
 	 	 	 	 	 K e y w o r d s . Q u o t e ,   f i e l d . I d ,   K e y w o r d s . Q u o t e ,   " ,   " ,  
 	 	 	 	 	 c l a s s N a m e ,   " . " ,   K e y w o r d s . F u n c P r e f i x ,   p r o p N a m e ,   " ,   " ,  
 	 	 	 	 	 c l a s s N a m e ,   " . " ,   K e y w o r d s . E x p r P r e f i x ,   p r o p N a m e ,   " ) ; " ) ;  
 	 	 	 }  
  
 	 	 	 p r o t e c t e d   o v e r r i d e   v o i d   E m i t P r o p e r t y S e t t e r ( S t r u c t u r e d T y p e F i e l d   f i e l d ,   s t r i n g   t y p e N a m e ,   s t r i n g   p r o p N a m e )  
 	 	 	 {  
 	 	 	 	 i f   ( f i e l d . R e l a t i o n   = =   F i e l d R e l a t i o n . C o l l e c t i o n )  
 	 	 	 	 {  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e B e g i n S e t t e r   ( C o d e H e l p e r . P u b l i c A t t r i b u t e s ) ;  
  
 	 	 	 	 	 i f   ( ( f i e l d . D e f i n i n g T y p e I d . I s E m p t y )   | |  
 	 	 	 	 	 	 ( f i e l d . M e m b e r s h i p   = =   F i e l d M e m b e r s h i p . L o c a l O v e r r i d e ) )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 / / 	 T h e   f i e l d   i s   d e f i n e d   l o c a l l y   a n d   i s   n o t   t h e   i m p l e m e n t a t i o n   o f   a n y  
 	 	 	 	 	 	 / / 	 i n t e r f a c e .  
  
 	 	 	 	 	 	 i f   ( f i e l d . O p t i o n s . H a s F l a g   ( F i e l d O p t i o n s . V i r t u a l ) )  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 / /   T h e   f i e l d   i s   v i r t u a l   a n d   w e   m u s t   s e t   i t s   v a l u e   w i t h   a   p a r t i a l   m e t h o d .  
  
 	 	 	 	 	 	 	 v a r   m e t h o d N a m e   =   t h i s . g e n e r a t o r . C r e a t e M e t h o d N a m e F o r V i r t u a l P r o p e r t y S e t t e r   ( f i e l d . C a p t i o n I d ) ;  
  
 	 	 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e C o d e L i n e   ( t y p e N a m e ,   "   " ,   K e y w o r d s . O l d V a l u e V a r i a b l e ,   "   =   " ,   K e y w o r d s . T h i s ,   " . " ,   p r o p N a m e ,   " ; " ) ;  
 	 	 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e C o d e L i n e   ( K e y w o r d s . I f ,   "   ( " ,   K e y w o r d s . O l d V a l u e V a r i a b l e ,   "   ! =   " ,   K e y w o r d s . V a l u e V a r i a b l e ,   "   | |   " ,   " ! " ,   K e y w o r d s . T h i s ,   " . " ,   K e y w o r d s . I s F i e l d D e f i n e d M e t h o d ,   " ( " ,   K e y w o r d s . Q u o t e ,   f i e l d . I d ,   K e y w o r d s . Q u o t e ,   " ) " ,   " ) " ) ;  
 	 	 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e B e g i n B l o c k   ( ) ;  
 	 	 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e C o d e L i n e   ( K e y w o r d s . T h i s ,   " . " ,   K e y w o r d s . O n P r e f i x ,   p r o p N a m e ,   K e y w o r d s . C h a n g i n g S u f f i x ,   "   ( " ,   K e y w o r d s . O l d V a l u e V a r i a b l e ,   " ,   " ,   K e y w o r d s . V a l u e V a r i a b l e ,   " ) " ,   " ; " ) ;  
 	 	 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e C o d e L i n e   ( K e y w o r d s . T h i s ,   " . " ,   m e t h o d N a m e ,   "   ( " ,   K e y w o r d s . V a l u e V a r i a b l e ,   " ) " ,   " ; " ) ;  
 	 	 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e C o d e L i n e   ( K e y w o r d s . T h i s ,   " . " ,   K e y w o r d s . O n P r e f i x ,   p r o p N a m e ,   K e y w o r d s . C h a n g e d S u f f i x ,   "   ( " ,   K e y w o r d s . O l d V a l u e V a r i a b l e ,   " ,   " ,   K e y w o r d s . V a l u e V a r i a b l e ,   " ) " ,   " ; " ) ;  
 	 	 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e E n d B l o c k   ( ) ;  
 	 	 	 	 	 	 }  
 	 	 	 	 	 	 e l s e  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 / /   T h e   f i e l d   i s   n o t   v i r t u a l   t h u s   w e   m u s t   s e t   i t s   v a l u e   t h e   r e g u l a r   w a y .  
  
 	 	 	 	 	 	 	 s w i t c h   ( f i e l d . S o u r c e )  
 	 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 	 c a s e   F i e l d S o u r c e . V a l u e :  
 	 	 	 	 	 	 	 	 	 t h i s . E m i t V a l u e S e t t e r   ( f i e l d ,   t y p e N a m e ,   p r o p N a m e ) ;  
 	 	 	 	 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 	 	 	 	 c a s e   F i e l d S o u r c e . E x p r e s s i o n :  
 	 	 	 	 	 	 	 	 	 t h i s . E m i t E x p r e s s i o n S e t t e r   ( f i e l d ,   t y p e N a m e ,   p r o p N a m e ) ;  
 	 	 	 	 	 	 	 	 	 b r e a k ;  
 	 	 	 	 	 	 	 }  
 	 	 	 	 	 	 }  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e  
 	 	 	 	 	 {  
 	 	 	 	 	 	 / / 	 T h e   f i e l d   i s   d e f i n e d   b y   a n   i n t e r f a c e   a n d   t h i s   i s   a n   i m p l e m e n t a t i o n  
 	 	 	 	 	 	 / / 	 o f   t h e   s e t t e r .  
  
 	 	 	 	 	 	 s t r i n g   i n t e r f a c e N a m e   =   t h i s . g e n e r a t o r . C r e a t e T y p e F u l l N a m e   ( f i e l d . D e f i n i n g T y p e I d ) ;  
 	 	 	 	 	 	 s t r i n g   i n t e r f a c e I m p l e m e n t a t i o n N a m e   =   C o d e G e n e r a t o r . C r e a t e I n t e r f a c e I m p l e m e n t a t i o n I d e n t i f i e r   ( i n t e r f a c e N a m e ) ;  
 	 	 	 	 	 	 s t r i n g   s e t t e r M e t h o d N a m e   =   s t r i n g . C o n c a t   ( K e y w o r d s . I n t e r f a c e I m p l e m e n t a t i o n S e t t e r M e t h o d P r e f i x ,   p r o p N a m e ) ;  
  
 	 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e C o d e L i n e   ( i n t e r f a c e I m p l e m e n t a t i o n N a m e ,   " . " ,   s e t t e r M e t h o d N a m e ,   "   ( " ,   K e y w o r d s . T h i s ,   " ,   " ,   K e y w o r d s . V a l u e V a r i a b l e ,   " ) ; " ) ;  
 	 	 	 	 	 }  
  
 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e E n d S e t t e r   ( ) ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 p r i v a t e   v o i d   E m i t V a l u e S e t t e r ( S t r u c t u r e d T y p e F i e l d   f i e l d ,   s t r i n g   t y p e N a m e ,   s t r i n g   p r o p N a m e )  
 	 	 	 {  
 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e C o d e L i n e   ( t y p e N a m e ,   "   " ,   K e y w o r d s . O l d V a l u e V a r i a b l e ,   "   =   " ,   K e y w o r d s . T h i s ,   " . " ,   p r o p N a m e ,   " ; " ) ;  
 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e C o d e L i n e   ( K e y w o r d s . I f ,   "   ( " ,   K e y w o r d s . O l d V a l u e V a r i a b l e ,   "   ! =   " ,   K e y w o r d s . V a l u e V a r i a b l e ,   "   | |   " ,   " ! " ,   K e y w o r d s . T h i s ,   " . " ,   K e y w o r d s . I s F i e l d D e f i n e d M e t h o d ,   " ( " ,   K e y w o r d s . Q u o t e ,   f i e l d . I d ,   K e y w o r d s . Q u o t e ,   " ) " ,   " ) " ) ;  
 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e B e g i n B l o c k   ( ) ;  
 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e C o d e L i n e   ( K e y w o r d s . T h i s ,   " . " ,   K e y w o r d s . O n P r e f i x ,   p r o p N a m e ,   K e y w o r d s . C h a n g i n g S u f f i x ,   "   ( " ,   K e y w o r d s . O l d V a l u e V a r i a b l e ,   " ,   " ,   K e y w o r d s . V a l u e V a r i a b l e ,   " ) ; " ) ;  
 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e C o d e L i n e   ( K e y w o r d s . T h i s ,   " . " ,   K e y w o r d s . S e t F i e l d M e t h o d ,   " < " ,   t y p e N a m e ,   " >   ( " ,   K e y w o r d s . Q u o t e ,   f i e l d . I d ,   K e y w o r d s . Q u o t e ,   " ,   " ,   K e y w o r d s . O l d V a l u e V a r i a b l e ,   " ,   " ,   K e y w o r d s . V a l u e V a r i a b l e ,   " ) ; " ) ;  
 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e C o d e L i n e   ( K e y w o r d s . T h i s ,   " . " ,   K e y w o r d s . O n P r e f i x ,   p r o p N a m e ,   K e y w o r d s . C h a n g e d S u f f i x ,   "   ( " ,   K e y w o r d s . O l d V a l u e V a r i a b l e ,   " ,   " ,   K e y w o r d s . V a l u e V a r i a b l e ,   " ) ; " ) ;  
 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e E n d B l o c k   ( ) ;  
 	 	 	 }  
 	 	 	  
 	 	 	 p r i v a t e   v o i d   E m i t E x p r e s s i o n S e t t e r ( S t r u c t u r e d T y p e F i e l d   f i e l d ,   s t r i n g   t y p e N a m e ,   s t r i n g   p r o p N a m e )  
 	 	 	 {  
 	 	 	 	 s t r i n g   c l a s s N a m e   =   t h i s . g e n e r a t o r . C r e a t e E n t i t y F u l l N a m e   ( t h i s . t y p e . C a p t i o n I d ) ;  
  
 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e C o d e L i n e   (  
 	 	 	 	 	 K e y w o r d s . G l o b a l ,   " : : " ,   K e y w o r d s . A b s t r a c t E n t i t y ,   " . " ,   K e y w o r d s . S e t C a l c u l a t i o n M e t h o d ,   " < " ,   c l a s s N a m e ,   " ,   " ,   t y p e N a m e ,   " >   ( " ,  
 	 	 	 	 	 K e y w o r d s . T h i s ,   " ,   " ,  
 	 	 	 	 	 K e y w o r d s . Q u o t e ,   f i e l d . I d ,   K e y w o r d s . Q u o t e ,   " ,   " ,  
 	 	 	 	 	 K e y w o r d s . V a l u e V a r i a b l e ,   " ) ; " ) ;  
 	 	 	 }  
  
 	 	 	 p r o t e c t e d   o v e r r i d e   v o i d   E m i t P r o p e r t y O n C h a n g e d ( S t r u c t u r e d T y p e F i e l d   f i e l d ,   s t r i n g   t y p e N a m e ,   s t r i n g   p r o p N a m e )  
 	 	 	 {  
 	 	 	 	 i f   ( f i e l d . S o u r c e   = =   F i e l d S o u r c e . V a l u e )  
 	 	 	 	 {  
 	 	 	 	 	 s t r i n g   c o d e   =   s t r i n g . C o n c a t   ( K e y w o r d s . V o i d ,   "   " ,   K e y w o r d s . O n P r e f i x ,   p r o p N a m e ,   K e y w o r d s . C h a n g e d S u f f i x ,   " ( " ,  
 	 	 	 	 	 	 / * * / 	 	 	 	 	   t y p e N a m e ,   "   " ,   K e y w o r d s . O l d V a l u e V a r i a b l e ,   " ,   " ,  
 	 	 	 	 	 	 / * * / 	 	 	 	           t y p e N a m e ,   "   " ,   K e y w o r d s . N e w V a l u e V a r i a b l e ,   " ) " ) ;  
 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e B e g i n M e t h o d   ( C o d e H e l p e r . P a r t i a l M e t h o d A t t r i b u t e s ,   c o d e ) ;  
 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e E n d M e t h o d   ( ) ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 p r o t e c t e d   o v e r r i d e   v o i d   E m i t P r o p e r t y O n C h a n g i n g ( S t r u c t u r e d T y p e F i e l d   f i e l d ,   s t r i n g   t y p e N a m e ,   s t r i n g   p r o p N a m e )  
 	 	 	 {  
 	 	 	 	 i f   ( f i e l d . S o u r c e   = =   F i e l d S o u r c e . V a l u e )  
 	 	 	 	 {  
 	 	 	 	 	 s t r i n g   c o d e   =   s t r i n g . C o n c a t   ( K e y w o r d s . V o i d ,   "   " ,   K e y w o r d s . O n P r e f i x ,   p r o p N a m e ,   K e y w o r d s . C h a n g i n g S u f f i x ,   " ( " ,  
 	 	 	 	 	 	 / * * / 	 	 	 	 	   t y p e N a m e ,   "   " ,   K e y w o r d s . O l d V a l u e V a r i a b l e ,   " ,   " ,  
 	 	 	 	 	 	 / * * / 	 	 	 	           t y p e N a m e ,   "   " ,   K e y w o r d s . N e w V a l u e V a r i a b l e ,   " ) " ) ;  
 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e B e g i n M e t h o d   ( C o d e H e l p e r . P a r t i a l M e t h o d A t t r i b u t e s ,   c o d e ) ;  
 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e E n d M e t h o d   ( ) ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 p r o t e c t e d   o v e r r i d e   v o i d   E m i t M e t h o d F o r V i r t u a l P r o p e r t y G e t t e r ( S t r u c t u r e d T y p e F i e l d   f i e l d ,   s t r i n g   f i e l d T y p e N a m e )  
 	 	 	 {  
 	 	 	 	 s t r i n g   m e t h o d N a m e   =   t h i s . g e n e r a t o r . C r e a t e M e t h o d N a m e F o r V i r t u a l P r o p e r t y G e t t e r   ( f i e l d . C a p t i o n I d ) ;  
  
 	 	 	 	 s t r i n g   c o d e   =   K e y w o r d s . V o i d   +   "   "   +   m e t h o d N a m e   +   " ( "   +   K e y w o r d s . R e f   +   "   "   +   f i e l d T y p e N a m e   +   "   "   +   K e y w o r d s . V a l u e V a r i a b l e   +   " ) " ;  
 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e B e g i n M e t h o d   ( C o d e H e l p e r . P a r t i a l M e t h o d A t t r i b u t e s ,   c o d e ) ;  
 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e E n d M e t h o d   ( ) ;  
 	 	 	 }  
  
 	 	 	 p r o t e c t e d   o v e r r i d e   v o i d   E m i t M e t h o d F o r V i r t u a l P r o p e r t y S e t t e r ( S t r u c t u r e d T y p e F i e l d   f i e l d ,   s t r i n g   f i e l d T y p e N a m e )  
 	 	 	 {  
 	 	 	 	 s t r i n g   m e t h o d N a m e   =   t h i s . g e n e r a t o r . C r e a t e M e t h o d N a m e F o r V i r t u a l P r o p e r t y S e t t e r   ( f i e l d . C a p t i o n I d ) ;  
  
 	 	 	 	 s t r i n g   c o d e   =   K e y w o r d s . V o i d   +   "   "   +   m e t h o d N a m e   +   " ( "   +   f i e l d T y p e N a m e   +   "   "   +   K e y w o r d s . V a l u e V a r i a b l e   +   " ) " ;  
 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e B e g i n M e t h o d   ( C o d e H e l p e r . P a r t i a l M e t h o d A t t r i b u t e s ,   c o d e ) ;  
 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e E n d M e t h o d   ( ) ;  
 	 	 	 }  
  
 	 	 	 p r i v a t e   r e a d o n l y   H a s h S e t < D r u i d >   b a s e E n t i t y I d s ;  
 	 	 }  
  
 	 	 # e n d r e g i o n  
  
 	 	 # r e g i o n   I n t e r f a c e E m i t t e r   C l a s s  
  
 	 	 p r i v a t e   s e a l e d   c l a s s   I n t e r f a c e E m i t t e r   :   E m i t t e r  
 	 	 {  
 	 	 	 p u b l i c   I n t e r f a c e E m i t t e r ( C o d e G e n e r a t o r   g e n e r a t o r ,   S t r u c t u r e d T y p e   t y p e )  
 	 	 	 	 :   b a s e   ( g e n e r a t o r ,   t y p e )  
 	 	 	 {  
 	 	 	 }  
  
 	 	 	 p r o t e c t e d   o v e r r i d e   b o o l   S u p p r e s s F i e l d ( S t r u c t u r e d T y p e F i e l d   f i e l d )  
 	 	 	 {  
 	 	 	 	 / / 	 D o n ' t   g e n e r a t e   f i e l d s   d e f i n e d   i n   i n h e r i t e d   i n t e r f a c e s .  
 	 	 	 	 D r u i d   i n t e r f a c e I d   =   f i e l d   = =   n u l l   ?   D r u i d . E m p t y   :   f i e l d . D e f i n i n g T y p e I d ;  
 	 	 	 	 r e t u r n   i n t e r f a c e I d . I s V a l i d ;  
 	 	 	 }  
  
 	 	 	 p r o t e c t e d   o v e r r i d e   v o i d   E m i t P r o p e r t y G e t t e r ( S t r u c t u r e d T y p e F i e l d   f i e l d ,   s t r i n g   t y p e N a m e ,   s t r i n g   p r o p N a m e )  
 	 	 	 {  
 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e B e g i n G e t t e r   ( C o d e H e l p e r . P u b l i c A t t r i b u t e s ) ;  
 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e E n d G e t t e r   ( ) ;  
 	 	 	 }  
  
 	 	 	 p r o t e c t e d   o v e r r i d e   v o i d   E m i t P r o p e r t y S e t t e r ( S t r u c t u r e d T y p e F i e l d   f i e l d ,   s t r i n g   t y p e N a m e ,   s t r i n g   p r o p N a m e )  
 	 	 	 {  
 	 	 	 	 i f   ( f i e l d . R e l a t i o n   = =   F i e l d R e l a t i o n . C o l l e c t i o n )  
 	 	 	 	 {  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e B e g i n S e t t e r   ( C o d e H e l p e r . P u b l i c A t t r i b u t e s ) ;  
 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e E n d S e t t e r   ( ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 # e n d r e g i o n  
  
 	 	 # r e g i o n   I n t e r f a c e I m p l e m e n t a t i o n E m i t t e r   C l a s s  
  
 	 	 p r i v a t e   s e a l e d   c l a s s   I n t e r f a c e I m p l e m e n t a t i o n E m i t t e r   :   E m i t t e r  
 	 	 {  
 	 	 	 p u b l i c   I n t e r f a c e I m p l e m e n t a t i o n E m i t t e r ( C o d e G e n e r a t o r   g e n e r a t o r ,   S t r u c t u r e d T y p e   t y p e )  
 	 	 	 	 :   b a s e   ( g e n e r a t o r ,   t y p e )  
 	 	 	 {  
 	 	 	 	 t h i s . c l a s s N a m e   =   C o d e G e n e r a t o r . C r e a t e I n t e r f a c e I m p l e m e n t a t i o n I d e n t i f i e r   ( t y p e . C a p t i o n . N a m e ) ;  
 	 	 	 	 t h i s . i n t e r f a c e N a m e   =   t h i s . g e n e r a t o r . C r e a t e E n t i t y F u l l N a m e   ( t y p e . C a p t i o n I d ) ;  
 	 	 	 }  
  
 	 	 	 p r o t e c t e d   o v e r r i d e   b o o l   S u p p r e s s F i e l d ( S t r u c t u r e d T y p e F i e l d   f i e l d )  
 	 	 	 {  
 	 	 	 	 / / 	 D o n ' t   g e n e r a t e   f i e l d s   d e f i n e d   i n   i n h e r i t e d   i n t e r f a c e s .  
 	 	 	 	 D r u i d   i n t e r f a c e I d   =   f i e l d   = =   n u l l   ?   D r u i d . E m p t y   :   f i e l d . D e f i n i n g T y p e I d ;  
 	 	 	 	 r e t u r n   i n t e r f a c e I d . I s V a l i d ;  
 	 	 	 }  
  
 	 	 	 p r i v a t e   v o i d   E m i t E x p r e s s i o n ( S t r u c t u r e d T y p e F i e l d   f i e l d ,   s t r i n g   t y p e N a m e ,   s t r i n g   p r o p N a m e )  
 	 	 	 {  
 	 	 	 	 E n t i t y E x p r e s s i o n   e x p r e s s i o n   =   E n t i t y E x p r e s s i o n . F r o m E n c o d e d E x p r e s s i o n   ( f i e l d . E x p r e s s i o n ) ;  
  
 	 	 	 	 i f   ( e x p r e s s i o n . E n c o d i n g   ! =   E n t i t y E x p r e s s i o n E n c o d i n g . L a m b d a C S h a r p S o u r c e C o d e )  
 	 	 	 	 {  
 	 	 	 	 	 t h r o w   n e w   S y s t e m . I n v a l i d O p e r a t i o n E x c e p t i o n   ( s t r i n g . F o r m a t   ( " I n v a l i d   e x p r e s s i o n   e n c o d i n g   ' { 0 } '   f o r   f i e l d   { 1 } . { 2 } " ,   e x p r e s s i o n . E n c o d i n g ,   t h i s . t y p e . N a m e ,   p r o p N a m e ) ) ;  
 	 	 	 	 }  
  
 	 	 	 	 s t r i n g   c l a s s N a m e   =   t h i s . g e n e r a t o r . C r e a t e E n t i t y F u l l N a m e   ( t h i s . t y p e . C a p t i o n I d ) ;  
 	 	 	 	 s t r i n g   c o m m e n t   =   s t r i n g . C o n c a t   ( "   " ,   K e y w o r d s . S i m p l e C o m m e n t ,   "   »  " ,   t h i s . t y p e . C a p t i o n I d . T o S t r i n g   ( ) ,   "   " ,   f i e l d . I d ) ;  
  
 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e F i e l d   ( C o d e H e l p e r . I n t e r n a l S t a t i c R e a d O n l y F i e l d A t t r i b u t e s ,  
 	 	 	 	 	 K e y w o r d s . F u n c ,   " < " ,   c l a s s N a m e ,   " ,   " ,   t y p e N a m e ,   " >   " ,   K e y w o r d s . F u n c P r e f i x ,   p r o p N a m e ,   "   =   " ,  
 	 	 	 	 	 e x p r e s s i o n . S o u r c e C o d e ,   " ; " ,   c o m m e n t ) ;  
  
 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e F i e l d   ( C o d e H e l p e r . I n t e r n a l S t a t i c R e a d O n l y F i e l d A t t r i b u t e s ,  
 	 	 	 	 	 K e y w o r d s . L i n q E x p r e s s i o n ,   " < " ,   K e y w o r d s . F u n c ,   " < " ,   c l a s s N a m e ,   " ,   " ,   t y p e N a m e ,   " > >   " ,   K e y w o r d s . E x p r P r e f i x ,   p r o p N a m e ,   "   =   " ,  
 	 	 	 	 	 e x p r e s s i o n . S o u r c e C o d e ,   " ; " ,   c o m m e n t ) ;  
 	 	 	 }  
  
 	 	 	 p r i v a t e   v o i d   E m i t V a l u e G e t t e r ( s t r i n g   e n t i t y T y p e ,   S t r u c t u r e d T y p e F i e l d   f i e l d ,   s t r i n g   t y p e N a m e )  
 	 	 	 {  
 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e C o d e L i n e   ( e n t i t y T y p e ,   "   " ,   K e y w o r d s . E n t i t y V a r i a b l e ,   "   =   " ,   K e y w o r d s . O b j V a r i a b l e ,   "   " ,   K e y w o r d s . A s ,   "   " ,   e n t i t y T y p e ,   " ; " ) ;  
 	 	 	 	  
 	 	 	 	 i f   ( f i e l d . R e l a t i o n   = =   F i e l d R e l a t i o n . C o l l e c t i o n )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e C o d e L i n e   ( K e y w o r d s . R e t u r n ,   "   " ,   K e y w o r d s . E n t i t y V a r i a b l e ,   " . " ,   K e y w o r d s . G e t F i e l d C o l l e c t i o n M e t h o d ,   " < " ,   t y p e N a m e ,   " >   ( " ,   K e y w o r d s . Q u o t e ,   f i e l d . I d ,   K e y w o r d s . Q u o t e ,   " ) ; " ) ;  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e C o d e L i n e   ( K e y w o r d s . R e t u r n ,   "   " ,   K e y w o r d s . E n t i t y V a r i a b l e ,   " . " ,   K e y w o r d s . G e t F i e l d M e t h o d ,   " < " ,   t y p e N a m e ,   " >   ( " ,   K e y w o r d s . Q u o t e ,   f i e l d . I d ,   K e y w o r d s . Q u o t e ,   " ) ; " ) ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 p r i v a t e   v o i d   E m i t E x p r e s s i o n G e t t e r ( S t r u c t u r e d T y p e F i e l d   f i e l d ,   s t r i n g   t y p e N a m e ,   s t r i n g   p r o p N a m e )  
 	 	 	 {  
 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e C o d e L i n e   (  
 	 	 	 	 	 K e y w o r d s . R e t u r n ,   "   " ,  
 	 	 	 	 	 K e y w o r d s . G l o b a l ,   " : : " ,   K e y w o r d s . A b s t r a c t E n t i t y ,   " . " ,   K e y w o r d s . G e t C a l c u l a t i o n M e t h o d ,   " < " ,   t h i s . i n t e r f a c e N a m e ,   " ,   " ,   t y p e N a m e ,   " >   ( " ,  
 	 	 	 	 	 K e y w o r d s . O b j V a r i a b l e ,   " ,   " ,  
 	 	 	 	 	 K e y w o r d s . Q u o t e ,   f i e l d . I d ,   K e y w o r d s . Q u o t e ,   " ,   " ,  
 	 	 	 	 	 t h i s . c l a s s N a m e ,   " . " ,   K e y w o r d s . F u n c P r e f i x ,   p r o p N a m e ,   " ,   " ,  
 	 	 	 	 	 t h i s . c l a s s N a m e ,   " . " ,   K e y w o r d s . E x p r P r e f i x ,   p r o p N a m e ,   " ) ; " ) ;  
 	 	 	 }  
  
 	 	 	 p r i v a t e   v o i d   E m i t E x p r e s s i o n S e t t e r ( S t r u c t u r e d T y p e F i e l d   f i e l d ,   s t r i n g   t y p e N a m e ,   s t r i n g   p r o p N a m e )  
 	 	 	 {  
 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e C o d e L i n e   (  
 	 	 	 	 	 K e y w o r d s . G l o b a l ,   " : : " ,   K e y w o r d s . A b s t r a c t E n t i t y ,   " . " ,   K e y w o r d s . S e t C a l c u l a t i o n M e t h o d ,   " < " ,   t h i s . i n t e r f a c e N a m e ,   " ,   " ,   t y p e N a m e ,   " >   ( " ,  
 	 	 	 	 	 K e y w o r d s . O b j V a r i a b l e ,   " ,   " ,  
 	 	 	 	 	 K e y w o r d s . Q u o t e ,   f i e l d . I d ,   K e y w o r d s . Q u o t e ,   " ,   " ,  
 	 	 	 	 	 K e y w o r d s . V a l u e V a r i a b l e ,   " ) ; " ) ;  
 	 	 	 }  
  
  
 	 	 	 p u b l i c   v o i d   E m i t L o c a l P r o p e r t y I m p l e m e n t a t i o n ( S t r u c t u r e d T y p e F i e l d   f i e l d )  
 	 	 	 {  
 	 	 	 	 i f   ( f i e l d . M e m b e r s h i p   = =   F i e l d M e m b e r s h i p . L o c a l )  
 	 	 	 	 {  
 	 	 	 	 	 i f   ( t h i s . S u p p r e s s F i e l d   ( f i e l d ) )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 r e t u r n ;  
 	 	 	 	 	 }  
  
 	 	 	 	 	 s t r i n g   t y p e N a m e   =   t h i s . g e n e r a t o r . C r e a t e T y p e F u l l N a m e   ( f i e l d . T y p e I d ,   T y p e R o s e t t a . I s N u l l a b l e   ( f i e l d ) ) ;  
 	 	 	 	 	 s t r i n g   p r o p N a m e   =   t h i s . g e n e r a t o r . C r e a t e P r o p e r t y N a m e   ( f i e l d . C a p t i o n I d ) ;  
  
 	 	 	 	 	 s t r i n g   g e t t e r M e t h o d N a m e   =   s t r i n g . C o n c a t   ( K e y w o r d s . I n t e r f a c e I m p l e m e n t a t i o n G e t t e r M e t h o d P r e f i x ,   p r o p N a m e ) ;  
 	 	 	 	 	 s t r i n g   g e t t e r R e t u r n T y p e   =   t y p e N a m e ;  
  
 	 	 	 	 	 i f   ( f i e l d . R e l a t i o n   = =   F i e l d R e l a t i o n . C o l l e c t i o n )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 g e t t e r R e t u r n T y p e   =   s t r i n g . C o n c a t   ( K e y w o r d s . G l o b a l ,   " : : " ,   K e y w o r d s . G e n e r i c I L i s t ,   " < " ,   t y p e N a m e ,   " > " ) ;  
 	 	 	 	 	 }  
  
 	 	 	 	 	 s t r i n g   g e t t e r C o d e   =   s t r i n g . C o n c a t   ( g e t t e r R e t u r n T y p e ,   "   " ,   g e t t e r M e t h o d N a m e ,   " ( " ,   t h i s . i n t e r f a c e N a m e ,   "   " ,   K e y w o r d s . O b j V a r i a b l e ,   " ) " ) ;  
 	 	 	 	 	 s t r i n g   e n t i t y T y p e   =   s t r i n g . C o n c a t   ( K e y w o r d s . G l o b a l ,   " : : " ,   K e y w o r d s . A b s t r a c t E n t i t y ) ;  
 	 	 	 	 	  
 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e B e g i n M e t h o d   ( C o d e H e l p e r . P u b l i c S t a t i c M e t h o d A t t r i b u t e s ,   g e t t e r C o d e ) ;  
  
 	 	 	 	 	 s w i t c h   ( f i e l d . S o u r c e )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 c a s e   F i e l d S o u r c e . V a l u e :  
 	 	 	 	 	 	 	 t h i s . E m i t V a l u e G e t t e r   ( e n t i t y T y p e ,   f i e l d ,   t y p e N a m e ) ;  
 	 	 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 	 	 c a s e   F i e l d S o u r c e . E x p r e s s i o n :  
 	 	 	 	 	 	 	 t h i s . E m i t E x p r e s s i o n G e t t e r   ( f i e l d ,   t y p e N a m e ,   p r o p N a m e ) ;  
 	 	 	 	 	 	 	 b r e a k ;  
 	 	 	 	 	 }  
 	 	 	 	 	  
 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e E n d M e t h o d   ( ) ;  
  
 	 	 	 	 	 i f   ( f i e l d . R e l a t i o n   = =   F i e l d R e l a t i o n . C o l l e c t i o n )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 / / 	 D o n ' t   g e n e r a t e   a   s e t t e r   m e t h o d .  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e  
 	 	 	 	 	 {  
 	 	 	 	 	 	 s t r i n g   s e t t e r M e t h o d N a m e   =   s t r i n g . C o n c a t   ( K e y w o r d s . I n t e r f a c e I m p l e m e n t a t i o n S e t t e r M e t h o d P r e f i x ,   p r o p N a m e ) ;  
 	 	 	 	 	 	 s t r i n g   s e t t e r C o d e   =   s t r i n g . C o n c a t   ( K e y w o r d s . V o i d ,   "   " ,   s e t t e r M e t h o d N a m e ,   " ( " ,   t h i s . i n t e r f a c e N a m e ,   "   " ,   K e y w o r d s . O b j V a r i a b l e ,   " ,   " ,   t y p e N a m e ,   "   " ,   K e y w o r d s . V a l u e V a r i a b l e ,   " ) " ) ;  
  
 	 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e B e g i n M e t h o d   ( C o d e H e l p e r . P u b l i c S t a t i c M e t h o d A t t r i b u t e s ,   s e t t e r C o d e ) ;  
 	 	 	 	 	 	  
 	 	 	 	 	 	 i f   ( f i e l d . S o u r c e   = =   F i e l d S o u r c e . V a l u e )  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e C o d e L i n e   ( e n t i t y T y p e ,   "   " ,   K e y w o r d s . E n t i t y V a r i a b l e ,   "   =   " ,   K e y w o r d s . O b j V a r i a b l e ,   "   " ,   K e y w o r d s . A s ,   "   " ,   e n t i t y T y p e ,   " ; " ) ;  
 	 	 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e C o d e L i n e   ( t y p e N a m e ,   "   " ,   K e y w o r d s . O l d V a l u e V a r i a b l e ,   "   =   " ,   K e y w o r d s . O b j V a r i a b l e ,   " . " ,   p r o p N a m e ,   " ; " ) ;  
 	 	 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e C o d e L i n e   ( K e y w o r d s . I f ,   "   ( " ,   K e y w o r d s . O l d V a l u e V a r i a b l e ,   "   ! =   " ,   K e y w o r d s . V a l u e V a r i a b l e ,   "   | |   " ,   " ! " ,   K e y w o r d s . E n t i t y V a r i a b l e ,   " . " ,   K e y w o r d s . I s F i e l d D e f i n e d M e t h o d ,   " ( " ,   K e y w o r d s . Q u o t e ,   f i e l d . I d ,   K e y w o r d s . Q u o t e ,   " ) " ,   " ) " ) ;  
 	 	 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e B e g i n B l o c k   ( ) ;  
 	 	 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e C o d e L i n e   ( t h i s . c l a s s N a m e ,   " . " ,   K e y w o r d s . O n P r e f i x ,   p r o p N a m e ,   K e y w o r d s . C h a n g i n g S u f f i x ,   "   ( " ,   K e y w o r d s . O b j V a r i a b l e ,   " ,   " ,   K e y w o r d s . O l d V a l u e V a r i a b l e ,   " ,   " ,   K e y w o r d s . V a l u e V a r i a b l e ,   " ) ; " ) ;  
 	 	 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e C o d e L i n e   ( K e y w o r d s . E n t i t y V a r i a b l e ,   " . " ,   K e y w o r d s . S e t F i e l d M e t h o d ,   " < " ,   t y p e N a m e ,   " >   ( " ,   K e y w o r d s . Q u o t e ,   f i e l d . I d ,   K e y w o r d s . Q u o t e ,   " ,   " ,   K e y w o r d s . O l d V a l u e V a r i a b l e ,   " ,   " ,   K e y w o r d s . V a l u e V a r i a b l e ,   " ) ; " ) ;  
 	 	 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e C o d e L i n e   ( t h i s . c l a s s N a m e ,   " . " ,   K e y w o r d s . O n P r e f i x ,   p r o p N a m e ,   K e y w o r d s . C h a n g e d S u f f i x ,   "   ( " ,   K e y w o r d s . O b j V a r i a b l e ,   " ,   " ,   K e y w o r d s . O l d V a l u e V a r i a b l e ,   " ,   " ,   K e y w o r d s . V a l u e V a r i a b l e ,   " ) ; " ) ;  
 	 	 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e E n d B l o c k   ( ) ;  
 	 	 	 	 	 	 }  
 	 	 	 	 	 	 e l s e  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 t h i s . E m i t E x p r e s s i o n S e t t e r   ( f i e l d ,   t y p e N a m e ,   p r o p N a m e ) ;  
 	 	 	 	 	 	 }  
 	 	 	 	 	 	  
 	 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e E n d M e t h o d   ( ) ;  
  
 	 	 	 	 	 	 i f   ( f i e l d . S o u r c e   = =   F i e l d S o u r c e . V a l u e )  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 s t r i n g   c o d e ;  
  
 	 	 	 	 	 	 	 c o d e   =   s t r i n g . C o n c a t   ( K e y w o r d s . V o i d ,   "   " ,   K e y w o r d s . O n P r e f i x ,   p r o p N a m e ,   K e y w o r d s . C h a n g e d S u f f i x ,   " ( " ,  
 	 	 	 	 	 	 	 	 / * * / 	 	 	     t h i s . i n t e r f a c e N a m e ,   "   " ,   K e y w o r d s . O b j V a r i a b l e ,   " ,   " ,  
 	 	 	 	 	 	 	 	 / * * / 	 	 	     t y p e N a m e ,   "   " ,   K e y w o r d s . O l d V a l u e V a r i a b l e ,   " ,   " ,  
 	 	 	 	 	 	 	 	 / * * / 	 	 	     t y p e N a m e ,   "   " ,   K e y w o r d s . N e w V a l u e V a r i a b l e ,   " ) " ) ;  
 	 	 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e B e g i n M e t h o d   ( C o d e H e l p e r . P a r t i a l S t a t i c M e t h o d A t t r i b u t e s ,   c o d e ) ;  
 	 	 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e E n d M e t h o d   ( ) ;  
  
 	 	 	 	 	 	 	 c o d e   =   s t r i n g . C o n c a t   ( K e y w o r d s . V o i d ,   "   " ,   K e y w o r d s . O n P r e f i x ,   p r o p N a m e ,   K e y w o r d s . C h a n g i n g S u f f i x ,   " ( " ,  
 	 	 	 	 	 	 	 	 / * * / 	 	 	     t h i s . i n t e r f a c e N a m e ,   "   " ,   K e y w o r d s . O b j V a r i a b l e ,   " ,   " ,  
 	 	 	 	 	 	 	 	 / * * / 	 	 	     t y p e N a m e ,   "   " ,   K e y w o r d s . O l d V a l u e V a r i a b l e ,   " ,   " ,  
 	 	 	 	 	 	 	 	 / * * / 	 	 	     t y p e N a m e ,   "   " ,   K e y w o r d s . N e w V a l u e V a r i a b l e ,   " ) " ) ;  
 	 	 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e B e g i n M e t h o d   ( C o d e H e l p e r . P a r t i a l S t a t i c M e t h o d A t t r i b u t e s ,   c o d e ) ;  
 	 	 	 	 	 	 	 t h i s . g e n e r a t o r . f o r m a t t e r . W r i t e E n d M e t h o d   ( ) ;  
 	 	 	 	 	 	 }  
 	 	 	 	 	 }  
  
 	 	 	 	 	 s w i t c h   ( f i e l d . S o u r c e )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 c a s e   F i e l d S o u r c e . V a l u e :  
 	 	 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 	 	 c a s e   F i e l d S o u r c e . E x p r e s s i o n :  
 	 	 	 	 	 	 	 t h i s . E m i t E x p r e s s i o n   ( f i e l d ,   t y p e N a m e ,   p r o p N a m e ) ;  
 	 	 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 	 	 d e f a u l t :  
 	 	 	 	 	 	 	 t h r o w   n e w   S y s t e m . A r g u m e n t E x c e p t i o n   ( s t r i n g . F o r m a t   ( " F i e l d S o u r c e . { 0 }   n o t   v a l i d   i n   t h i s   c o n t e x t " ,   f i e l d . S o u r c e ) ) ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 p r i v a t e   s t r i n g   c l a s s N a m e ;  
 	 	 	 p r i v a t e   s t r i n g   i n t e r f a c e N a m e ;  
 	 	 }  
  
 	 	 # e n d r e g i o n  
  
 	 	 p r i v a t e   C o d e F o r m a t t e r   f o r m a t t e r ;  
 	 	 p r i v a t e   R e s o u r c e M a n a g e r   r e s o u r c e M a n a g e r ;  
 	 	 p r i v a t e   R e s o u r c e M a n a g e r P o o l   r e s o u r c e M a n a g e r P o o l ;  
 	 	 p r i v a t e   R e s o u r c e M o d u l e I n f o   r e s o u r c e M o d u l e I n f o ;  
 	 	 p r i v a t e   s t r i n g   s o u r c e N a m e s p a c e R e s ;  
 	 	 p r i v a t e   s t r i n g   s o u r c e N a m e s p a c e E n t i t i e s ;  
 	 }  
 } 