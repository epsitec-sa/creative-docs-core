ÿþu s i n g   S y s t e m . C o l l e c t i o n s . G e n e r i c ;  
 u s i n g   E p s i t e c . C o m m o n . W i d g e t s ;  
 u s i n g   E p s i t e c . C o m m o n . S u p p o r t ;  
 u s i n g   E p s i t e c . C o m m o n . D r a w i n g ;  
 u s i n g   E p s i t e c . C o m m o n . D o c u m e n t ;  
  
 n a m e s p a c e   E p s i t e c . C o m m o n . D o c u m e n t . C o n t a i n e r s  
 {  
 	 / / /   < s u m m a r y >  
 	 / / /   C e t t e   c l a s s e   g è r e   l e   p a n n e a u   d e s   v u e s   d e s   c a l q u e s   m i n i a t u r e s .  
 	 / / /   < / s u m m a r y >  
 	 p u b l i c   c l a s s   L a y e r M i n i a t u r e s  
 	 {  
 	 	 p u b l i c   L a y e r M i n i a t u r e s ( D o c u m e n t   d o c u m e n t )  
 	 	 {  
 	 	 	 t h i s . d o c u m e n t   =   d o c u m e n t ;  
  
 	 	 	 t h i s . l a y e r s   =   n e w   L i s t < i n t > ( ) ;  
 	 	 	 t h i s . l a y e r s T o R e g e n e r a t e   =   n e w   L i s t < i n t > ( ) ;  
 	 	 }  
  
  
 	 	 p u b l i c   b o o l   I s P a n e l S h o w e d  
 	 	 {  
 	 	 	 / / 	 I n d i q u e   s i   l e   p a n n e a u   d e s   m i n u a t u r e s   e s t   v i s i b l e   o u   c a c h é .  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . i s P a n e l S h o w e d ;  
 	 	 	 }  
 	 	 	 s e t  
 	 	 	 {  
 	 	 	 	 t h i s . i s P a n e l S h o w e d   =   v a l u e ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p u b l i c   v o i d   C r e a t e I n t e r f a c e ( F r a m e B o x   p a r e n t P a n e l )  
 	 	 {  
 	 	 	 / / 	 C r é e   l ' i n t e r f a c e   n é c e s s a i r e   p o u r   l e s   c a l q u e s   m i n i a t u r e s   d a n s   u n   p a n n e a u   d é d i é .  
 	 	 	 t h i s . p a r e n t P a n e l   =   p a r e n t P a n e l ;  
  
 	 	 	 / / 	 C r é e   p u i s   p e u p l e   l a   b a r r e   d ' o u t i l s .  
 	 	 	 H T o o l B a r   t o o l b a r   =   n e w   H T o o l B a r ( t h i s . p a r e n t P a n e l ) ;  
 	 	 	 t o o l b a r . P r e f e r r e d W i d t h   =   1 0 ;  
 	 	 	 t o o l b a r . D o c k   =   D o c k S t y l e . T o p ;  
 	 	 	 t o o l b a r . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   0 ,   - 1 ) ;  
  
 	 	 	 t h i s . s l i d e r   =   n e w   H S l i d e r ( ) ;  
 	 	 	 t h i s . s l i d e r . M i n V a l u e   =   1 M ;  
 	 	 	 t h i s . s l i d e r . M a x V a l u e   =   4 M ;  
 	 	 	 t h i s . s l i d e r . S m a l l C h a n g e   =   1 M ;  
 	 	 	 t h i s . s l i d e r . L a r g e C h a n g e   =   1 M ;  
 	 	 	 t h i s . s l i d e r . R e s o l u t i o n   =   ( t h i s . d o c u m e n t . T y p e   = =   D o c u m e n t T y p e . P i c t o g r a m )   ?   1 . 0 M   :   0 . 0 1 M ;  
 	 	 	 t h i s . s l i d e r . V a l u e   =   2 M ;  
 	 	 	 t h i s . s l i d e r . P r e f e r r e d W i d t h   =   6 0 ;  
 	 	 	 / / ? t h i s . s l i d e r . P r e f e r r e d H e i g h t   =   2 2 - 4 - 4 ;  
 	 	 	 t h i s . s l i d e r . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   4 ,   4 ) ;  
 	 	 	 t h i s . s l i d e r . D o c k   =   D o c k S t y l e . R i g h t ;  
 	 	 	 t h i s . s l i d e r . V a l u e C h a n g e d   + =   t h i s . H a n d l e S l i d e r V a l u e C h a n g e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . s l i d e r ,   R e s . S t r i n g s . C o n t a i n e r . L a y e r M i n i a t u r e s . S l i d e r . T o o l t i p ) ;  
 	 	 	 t o o l b a r . I t e m s . A d d ( t h i s . s l i d e r ) ;  
  
 	 	 	 / / 	 C r é e   l a   z o n e   c o n t e n a n t   l e s   m i n i a t u r e s .  
 	 	 	 t h i s . s c r o l l a b l e   =   n e w   S c r o l l a b l e ( t h i s . p a r e n t P a n e l ) ;  
 	 	 	 t h i s . s c r o l l a b l e . D o c k   =   D o c k S t y l e . F i l l ;  
 	 	 	 t h i s . s c r o l l a b l e . H o r i z o n t a l S c r o l l e r M o d e   =   S c r o l l a b l e S c r o l l e r M o d e . H i d e A l w a y s ;  
 	 	 	 t h i s . s c r o l l a b l e . V e r t i c a l S c r o l l e r M o d e   =   S c r o l l a b l e S c r o l l e r M o d e . S h o w A l w a y s ;  
 	 	 	 t h i s . s c r o l l a b l e . V i e w p o r t . I s A u t o F i t t i n g   =   t r u e ;  
 	 	 	 t h i s . s c r o l l a b l e . P a i n t V i e w p o r t F r a m e   =   t r u e ;  
 	 	 	 t h i s . s c r o l l a b l e . V i e w p o r t F r a m e M a r g i n s   =   n e w   M a r g i n s ( 0 ,   1 ,   0 ,   0 ) ;  
 	 	 	 t h i s . s c r o l l a b l e . V i e w p o r t P a d d i n g   =   n e w   M a r g i n s   ( - 1 ) ;  
 	 	 }  
  
  
 	 	 p u b l i c   v o i d   R e g e n e r a t e A l l ( )  
 	 	 {  
 	 	 	 / / 	 T o u t   d e v r a   ê t r e   r é g é n é r é   l o r s q u e   l e   t i m e r   a r r i v e r a   à   é c h é a n c e .  
 	 	 	 i n t   t o t a l   =   t h i s . T o t a l L a y e r s ;  
 	 	 	 f o r   ( i n t   l a y e r = 0 ;   l a y e r < t o t a l ;   l a y e r + + )  
 	 	 	 {  
 	 	 	 	 i f   ( ! t h i s . l a y e r s T o R e g e n e r a t e . C o n t a i n s ( l a y e r ) )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . l a y e r s T o R e g e n e r a t e . A d d ( l a y e r ) ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 t h i s . l a y e r s T o R e g e n e r a t e . S o r t ( ) ;  
 	 	 }  
  
 	 	 p u b l i c   v o i d   A d d L a y e r T o R e g e n e r a t e ( i n t   l a y e r )  
 	 	 {  
 	 	 	 / / 	 A j o u t e   u n   c a l q u e   d a n s   l a   l i s t e   d e s   c h o s e s   à   r é g é n é r e r   l o r s q u e   l e   t i m e r   a r r i v e r a   à   é c h é a n c e .  
 	 	 	 i f   ( ! t h i s . l a y e r s T o R e g e n e r a t e . C o n t a i n s ( l a y e r ) )  
 	 	 	 {  
 	 	 	 	 t h i s . l a y e r s T o R e g e n e r a t e . A d d ( l a y e r ) ;  
 	 	 	 	 t h i s . l a y e r s T o R e g e n e r a t e . S o r t ( ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p u b l i c   b o o l   T i m e E l a p s e d ( )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   l o r s q u e   l e   t i m e r   a r r i v e   à   é c h é a n c e .  
 	 	 	 / / 	 O n   r é g é n è r e   a l o r s   u n e   p a r t i e   d e   c e   q u e   l a   l i s t e   i n d i q u e .  
 	 	 	 / / 	 O n   r e t o u r n e   f a l s e   s ' i l   n ' y   a v a i t   p l u s   r i e n   à   r é g é n é r e r .  
 	 	 	 i n t   c u r r e n t L a y e r   =   t h i s . C u r r e n t L a y e r ;  
 	 	 	 i f   ( t h i s . l a y e r s T o R e g e n e r a t e . C o n t a i n s ( c u r r e n t L a y e r ) )  
 	 	 	 {  
 	 	 	 	 t h i s . R e g e n e r a t e L a y e r ( c u r r e n t L a y e r ) ;  
 	 	 	 	 r e t u r n   t r u e ;  
 	 	 	 }  
  
 	 	 	 i f   ( t h i s . l a y e r s T o R e g e n e r a t e . C o u n t   >   0 )  
 	 	 	 {  
 	 	 	 	 t h i s . R e g e n e r a t e L a y e r ( t h i s . l a y e r s T o R e g e n e r a t e [ 0 ] ) ;  
 	 	 	 	 r e t u r n   t r u e ;  
 	 	 	 }  
  
 	 	 	 r e t u r n   f a l s e ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   R e g e n e r a t e L a y e r ( i n t   l a y e r R a n k )  
 	 	 {  
 	 	 	 / / 	 R é g é n è r e   u n   c a l q u e   d o n n é .  
 	 	 	 O b j e c t s . A b s t r a c t   l a y e r   =   t h i s . G e t L a y e r ( l a y e r R a n k ) ;  
  
 	 	 	 i f   ( l a y e r   ! =   n u l l )  
 	 	 	 {  
 	 	 	 	 l a y e r . C a c h e B i t m a p D i r t y ( ) ;  
 	 	 	 	 t h i s . R e d r a w ( l a y e r R a n k ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . l a y e r s T o R e g e n e r a t e . R e m o v e ( l a y e r R a n k ) ;  
 	 	 }  
  
 	  
 	 	 p u b l i c   v o i d   U p d a t e L a y e r A f t e r C h a n g i n g ( )  
 	 	 {  
 	 	 	 / / 	 A d a p t e   l e s   m i n i a t u r e s   a p r è s   u n   c h a n g e m e n t   d e   c a l q u e   ( c r é a t i o n   d ' u n  
 	 	 	 / / 	 n o u v e a u   c a l q u e ,   s u p p r e s s i o n   d ' u n   c a l q u e ,   e t c . ) .  
 	 	 	 t h i s . l a y e r s . C l e a r ( ) ;  
  
 	 	 	 i n t   t o t a l   =   t h i s . T o t a l L a y e r s ;  
 	 	 	 f o r   ( i n t   i = t o t a l - 1 ;   i > = 0 ;   i - - )  
 	 	 	 {  
 	 	 	 	 t h i s . l a y e r s . A d d ( i ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . C r e a t e ( ) ;  
 	 	 }  
  
 	 	 p u b l i c   v o i d   R e d r a w ( i n t   l a y e r )  
 	 	 {  
 	 	 	 / / 	 R e d e s s i n e   u n   c a l q u e   q u i   a   c h a n g é .  
 	 	 	 f o r e a c h   ( V i e w e r   v i e w e r   i n   t h i s . d o c u m e n t . M o d i f i e r . V i e w e r s )  
 	 	 	 {  
 	 	 	 	 i f   ( v i e w e r . I s M i n i a t u r e   & &   v i e w e r . I s L a y e r M i n i a t u r e   & &   v i e w e r . D r a w i n g C o n t e x t . C u r r e n t L a y e r   = =   l a y e r )  
 	 	 	 	 {  
 	 	 	 	 	 v i e w e r . I n v a l i d a t e ( ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
  
 	 	 p r i v a t e   v o i d   H a n d l e S l i d e r V a l u e C h a n g e d ( o b j e c t   s e n d e r )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   l o r s q u e   l a   t a i l l e   d e s   m i n i a t u r e s   a   c h a n g é .  
 	 	 	 t h i s . C r e a t e ( ) ;  
 	 	 }  
  
  
 	 	 p r o t e c t e d   v o i d   C r e a t e ( )  
 	 	 {  
 	 	 	 / / 	 C r é e   t o u s   l e s   c a l q u e s   m i n i a t u r e s .  
 	 	 	 i f   ( t h i s . p a r e n t P a n e l . W i n d o w   = =   n u l l )     / /   i n i t i a l i s a t i o n   d u   l o g i c i e l   ?  
 	 	 	 {  
 	 	 	 	 r e t u r n ;  
 	 	 	 }  
  
 	 	 	 d o u b l e   o f f s e t Y   =   t h i s . s c r o l l a b l e . V i e w p o r t O f f s e t Y ;  
 	 	 	 t h i s . C l e a r ( ) ;     / /   s u p p r i m e   l e s   m i n i a t u r e s   e x i s t a n t e s  
  
 	 	 	 i f   ( ! t h i s . i s P a n e l S h o w e d )     / /   p a n n e a u   c a c h é   ?  
 	 	 	 {  
 	 	 	 	 r e t u r n ;  
 	 	 	 }  
  
 	 	 	 d o u b l e   z o o m   =   ( d o u b l e )   t h i s . s l i d e r . V a l u e ;  
 	 	 	 i n t   c u r r e n t P a g e   =   t h i s . C u r r e n t P a g e ;  
 	 	 	 i n t   c u r r e n t L a y e r   =   t h i s . C u r r e n t L a y e r ;  
 	 	 	 S i z e   p a g e S i z e   =   t h i s . d o c u m e n t . G e t P a g e S i z e ( c u r r e n t P a g e ) ;  
 	 	 	 d o u b l e   w ,   h ;  
 	 	 	 i f   ( t h i s . d o c u m e n t . T y p e   = =   D o c u m e n t T y p e . P i c t o g r a m )  
 	 	 	 {  
 	 	 	 	 w   =   S y s t e m . M a t h . C e i l i n g ( z o o m * p a g e S i z e . W i d t h ) + 2 ;  
 	 	 	 	 h   =   S y s t e m . M a t h . C e i l i n g ( z o o m * p a g e S i z e . H e i g h t ) + 2 ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 w   =   S y s t e m . M a t h . C e i l i n g ( z o o m * p a g e S i z e . W i d t h * 5 0 / 2 9 7 0 ) ;  
 	 	 	 	 h   =   S y s t e m . M a t h . C e i l i n g ( z o o m * p a g e S i z e . H e i g h t * 5 0 / 2 9 7 0 ) ;  
 	 	 	 }  
 	 	 	 d o u b l e   p o s Y   =   0 ;  
 	 	 	 d o u b l e   r e q u i r e d W i d t h   =   0 ;  
 	 	 	 L i s t < V i e w e r >   v i e w e r s   =   n e w   L i s t < V i e w e r > ( ) ;  
  
 	 	 	 f o r e a c h   ( i n t   l a y e r   i n   t h i s . l a y e r s )     / /   l a y e r   =   [ n - 1 . . 0 ]  
 	 	 	 {  
 	 	 	 	 O b j e c t s . L a y e r   o b j e c t L a y e r   =   t h i s . G e t L a y e r ( l a y e r ) ;  
 	 	 	 	 i f   ( o b j e c t L a y e r   = =   n u l l )  
 	 	 	 	 {  
 	 	 	 	 	 c o n t i n u e ;  
 	 	 	 	 }  
  
 	 	 	 	 i f   ( t h i s . d o c u m e n t . T y p e   = =   D o c u m e n t T y p e . P i c t o g r a m )  
 	 	 	 	 {  
 	 	 	 	 	 o b j e c t L a y e r . C a c h e B i t m a p S i z e   =   n e w   S i z e ( p a g e S i z e . W i d t h + 2 ,   p a g e S i z e . H e i g h t + 2 ) ;  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 o b j e c t L a y e r . C a c h e B i t m a p S i z e   =   n e w   S i z e ( w ,   h ) ;  
 	 	 	 	 }  
  
 	 	 	 	 s t r i n g   l a y e r N a m e   =   t h i s . L a y e r N a m e ( l a y e r ) ;  
  
 	 	 	 	 W i d g e t s . M i n i a t u r e F r a m e   b o x   =   n e w   W i d g e t s . M i n i a t u r e F r a m e ( t h i s . s c r o l l a b l e . V i e w p o r t ) ;  
 	 	 	 	 b o x . I s L e f t R i g h t P l a c e m e n t   =   f a l s e ;  
 	 	 	 	 b o x . I n d e x   =   l a y e r ;  
 	 	 	 	 b o x . P r e f e r r e d S i z e   =   n e w   S i z e ( w + 4 ,   h + 4 + L a y e r M i n i a t u r e s . l a b e l H e i g h t ) ;  
 	 	 	 	 b o x . P a d d i n g   =   n e w   M a r g i n s ( 2 ,   2 ,   2 ,   2 ) ;  
 	 	 	 	 b o x . A n c h o r   =   A n c h o r S t y l e s . T o p L e f t ;  
 	 	 	 	 b o x . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   p o s Y ,   0 ) ;  
 	 	 	 	 b o x . C l i c k e d   + =   t h i s . H a n d l e L a y e r B o x C l i c k e d ;  
 	 	 	 	 b o x . D r a g A n d D r o p D o i n g   + =   t h i s . H a n d l e D r a g A n d D r o p D o i n g ;  
 	 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( b o x ,   s t r i n g . F o r m a t ( R e s . S t r i n g s . C o n t a i n e r . L a y e r M i n i a t u r e s . B o x . T o o l t i p ,   l a y e r N a m e ) ) ;  
  
 	 	 	 	 / / 	 C r é e   l a   v u e   d u   c a l q u e   m i n i a t u r e .  
 	 	 	 	 V i e w e r   v i e w e r   =   n e w   V i e w e r ( t h i s . d o c u m e n t ) ;  
 	 	 	 	 v i e w e r . S e t P a r e n t ( b o x ) ;  
 	 	 	 	 v i e w e r . D o c k   =   D o c k S t y l e . F i l l ;  
 	 	 	 	 v i e w e r . P r e f e r r e d S i z e   =   n e w   S i z e ( w ,   h ) ;  
 	 	 	 	 i f   ( t h i s . d o c u m e n t . T y p e   = =   D o c u m e n t T y p e . P i c t o g r a m )  
 	 	 	 	 {  
 	 	 	 	 	 v i e w e r . P i c t o g r a m M i n i a t u r e Z o o m   =   z o o m ;  
 	 	 	 	 }  
 	 	 	 	 v i e w e r . I s M i n i a t u r e   =   t r u e ;  
 	 	 	 	 v i e w e r . I s L a y e r M i n i a t u r e   =   t r u e ;  
 	 	 	 	 v i e w e r . P a i n t P a g e F r a m e   =   f a l s e ;  
 	 	 	 	 v i e w e r . D r a w i n g C o n t e x t . L a y e r D r a w i n g M o d e   =   L a y e r D r a w i n g M o d e . H i d e I n a c t i v e ;  
 	 	 	 	 v i e w e r . D r a w i n g C o n t e x t . I n t e r n a l P a g e L a y e r ( c u r r e n t P a g e ,   l a y e r ) ;  
 	 	 	 	 v i e w e r . D r a w i n g C o n t e x t . G r i d S h o w   =   f a l s e ;  
 	 	 	 	 v i e w e r . D r a w i n g C o n t e x t . G u i d e s S h o w   =   f a l s e ;  
 	 	 	 	 v i e w e r . D r a w i n g C o n t e x t . T e x t S h o w C o n t r o l C h a r a c t e r s   =   f a l s e ;  
  
 	 	 	 	 / / 	 C r é e   l a   l é g e n d e   e n   b a s ,   a v e c   l e   l e t t r e + p o s i t i o n   d u   c a l q u e .  
 	 	 	 	 S t a t i c T e x t   l a b e l   =   n e w   S t a t i c T e x t ( b o x ) ;  
 	 	 	 	 l a b e l . P r e f e r r e d S i z e   =   n e w   S i z e ( w ,   L a y e r M i n i a t u r e s . l a b e l H e i g h t ) ;  
 	 	 	 	 l a b e l . D o c k   =   D o c k S t y l e . B o t t o m ;  
 	 	 	 	 l a b e l . C o n t e n t A l i g n m e n t   =   C o n t e n t A l i g n m e n t . T o p C e n t e r ;  
 	 	 	 	 l a b e l . T e x t   =   M i s c . F o n t S i z e ( l a y e r N a m e ,   0 . 7 5 ) ;  
  
 	 	 	 	 i f   ( l a y e r   = =   c u r r e n t L a y e r )  
 	 	 	 	 {  
 	 	 	 	 	 b o x . B a c k C o l o r   =   v i e w e r . D r a w i n g C o n t e x t . H i l i t e O u t l i n e C o l o r ;  
 	 	 	 	 }  
  
 	 	 	 	 p o s Y   + =   h + L a y e r M i n i a t u r e s . l a b e l H e i g h t + 2 ;  
 	 	 	 	 r e q u i r e d W i d t h   =   S y s t e m . M a t h . M a x ( r e q u i r e d W i d t h ,   w ) ;  
 	 	 	 	 v i e w e r s . A d d ( v i e w e r ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . s c r o l l a b l e . V i e w p o r t O f f s e t Y   =   o f f s e t Y ;  
 	 	 	 t h i s . p a r e n t P a n e l . P r e f e r r e d W i d t h   =   r e q u i r e d W i d t h + 3 + 2 3 ;  
 	 	 	 t h i s . p a r e n t P a n e l . W i n d o w . F o r c e L a y o u t ( ) ;  
  
 	 	 	 f o r e a c h   ( V i e w e r   v i e w e r   i n   v i e w e r s )  
 	 	 	 {  
 	 	 	 	 v i e w e r . D r a w i n g C o n t e x t . Z o o m P a g e A n d C e n t e r ( ) ;  
 	 	 	 	 t h i s . d o c u m e n t . M o d i f i e r . A t t a c h V i e w e r ( v i e w e r ) ;  
 	 	 	 	 t h i s . d o c u m e n t . N o t i f i e r . N o t i f y A r e a ( v i e w e r ) ;  
 	 	 	 }  
 	 	 }  
  
  
 	 	 p r o t e c t e d   v o i d   C l e a r ( )  
 	 	 {  
 	 	 	 / / 	 S u p p r i m e   t o u t e s   l e s   m i n i a t u r e s   d e s   c a l q u e s .  
 	 	 	 L i s t < V i e w e r >   v i e w e r s   =   n e w   L i s t < V i e w e r > ( ) ;  
 	 	 	 f o r e a c h   ( V i e w e r   v i e w e r   i n   t h i s . d o c u m e n t . M o d i f i e r . V i e w e r s )  
 	 	 	 {  
 	 	 	 	 i f   ( v i e w e r . I s M i n i a t u r e   & &   v i e w e r . I s L a y e r M i n i a t u r e )     / /   m i n i a t u r e   d ' u n   c a l q u e   ?  
 	 	 	 	 {  
 	 	 	 	 	 v i e w e r s . A d d ( v i e w e r ) ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 f o r e a c h   ( V i e w e r   v i e w e r   i n   v i e w e r s )  
 	 	 	 {  
 	 	 	 	 t h i s . d o c u m e n t . M o d i f i e r . D e t a c h V i e w e r ( v i e w e r ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . s c r o l l a b l e . V i e w p o r t . C h i l d r e n . C l e a r ( ) ;  
 	 	 }  
  
  
 	 	 p r i v a t e   v o i d   H a n d l e L a y e r B o x C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 U n   c a l q u e   m i n i a t u r e   a   é t é   c l i q u é .  
 	 	 	 W i d g e t s . M i n i a t u r e F r a m e   b o x   =   s e n d e r   a s   W i d g e t s . M i n i a t u r e F r a m e ;  
 	 	 	 t h i s . C u r r e n t L a y e r   =   b o x . I n d e x ;     / /   r e n d   l e   c a l q u e   c l i q u é   a c t i f  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e D r a g A n d D r o p D o i n g ( o b j e c t   s e n d e r ,   W i d g e t s . M i n i a t u r e F r a m e E v e n t A r g s   d s t )  
 	 	 {  
 	 	 	 / / 	 U n   c a l q u e   a   é t é   d r a g g é   s u r   u n   a u t r e .  
 	 	 	 W i d g e t s . M i n i a t u r e F r a m e   s r c   =   s e n d e r   a s   W i d g e t s . M i n i a t u r e F r a m e ;  
  
 	 	 	 i n t   l a y e r   =   d s t . F r a m e . I n d e x ;  
  
 	 	 	 i f   ( ! d s t . F r a m e . I s B e f o r e )  
 	 	 	 {  
 	 	 	 	 l a y e r + + ;  
 	 	 	 }  
  
 	 	 	 i f   ( ! s r c . I s D u p l i c a t e   & &   d s t . F r a m e . I n d e x   >   s r c . I n d e x )  
 	 	 	 {  
 	 	 	 	 l a y e r - - ;  
 	 	 	 }  
  
 	 	 	 i f   ( s r c . I s D u p l i c a t e )  
 	 	 	 {  
 	 	 	 	 t h i s . d o c u m e n t . M o d i f i e r . L a y e r D u p l i c a t e ( s r c . I n d e x ,   l a y e r ,   " " ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . d o c u m e n t . M o d i f i e r . L a y e r S w a p ( s r c . I n d e x ,   l a y e r ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . C u r r e n t L a y e r   =   l a y e r ;  
 	 	 }  
  
  
 	 	 p r o t e c t e d   s t r i n g   L a y e r N a m e ( i n t   r a n k )  
 	 	 {  
 	 	 	 / / 	 R e t o u r n e   l e   n o m   l e   p l u s   c o m p l e t   p o s s i b l e   d u   c a l q u e ,   c o n s t i t u é   d e   l a   l e t t r e  
 	 	 	 / / 	 s u i v i   d e   s a   p o s i t i o n   o u   d e   s o n   n o m .  
 	 	 	 O b j e c t s . L a y e r   l a y e r   =   t h i s . G e t L a y e r ( r a n k ) ;  
 	 	 	 i f   ( l a y e r   = =   n u l l )  
 	 	 	 {  
 	 	 	 	 r e t u r n   n u l l ;  
 	 	 	 }  
  
 	 	 	 s t r i n g   l a y e r N a m e   =   l a y e r . N a m e ;  
 	 	 	 i f   ( s t r i n g . I s N u l l O r E m p t y ( l a y e r N a m e ) )  
 	 	 	 {  
 	 	 	 	 r e t u r n   s t r i n g . C o n c a t ( " < b > " ,   O b j e c t s . L a y e r . S h o r t N a m e ( r a n k ) ,   " < / b >   " ,   O b j e c t s . L a y e r . L a y e r P o s i t i o n N a m e ( r a n k ,   t h i s . T o t a l L a y e r s ) ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 r e t u r n   s t r i n g . C o n c a t ( " < b > " ,   O b j e c t s . L a y e r . S h o r t N a m e ( r a n k ) ,   " < / b >   " ,   l a y e r N a m e ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   i n t   T o t a l L a y e r s  
 	 	 {  
 	 	 	 / / 	 R e t o u r n e   l e   n o m b r e   t o t a l   d e   c a l q u e s .  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . d o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t . T o t a l L a y e r s ( ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   i n t   C u r r e n t P a g e  
 	 	 {  
 	 	 	 / / 	 R e t o u r n e   l e   r a n g   d e   l a   p a g e   c o u r a n t e .  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . d o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t . C u r r e n t P a g e ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   i n t   C u r r e n t L a y e r  
 	 	 {  
 	 	 	 / / 	 R e t o u r n e   l e   r a n g   d u   c a l q u e   c o u r a n t .  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . d o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t . C u r r e n t L a y e r ;  
 	 	 	 }  
 	 	 	 s e t  
 	 	 	 {  
 	 	 	 	 t h i s . d o c u m e n t . M o d i f i e r . A c t i v e V i e w e r . D r a w i n g C o n t e x t . C u r r e n t L a y e r   =   v a l u e ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   O b j e c t s . L a y e r   G e t L a y e r ( i n t   r a n k )  
 	 	 {  
 	 	 	 / / 	 R e t o u r n e   u n   o b j e t   L a y e r   d u   d o c u m e n t .  
 	 	 	 U n d o a b l e L i s t   d o c   =   t h i s . d o c u m e n t . D o c u m e n t O b j e c t s ;  
 	 	 	 O b j e c t s . P a g e   p a g e   =   d o c [ t h i s . C u r r e n t P a g e ]   a s   O b j e c t s . P a g e ;  
  
 	 	 	 i f   ( r a n k   <   p a g e . O b j e c t s . C o u n t )  
 	 	 	 {  
 	 	 	 	 r e t u r n   p a g e . O b j e c t s [ r a n k ]   a s   O b j e c t s . L a y e r ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 r e t u r n   n u l l ;  
 	 	 	 }  
 	 	 }  
  
  
 	 	 p r o t e c t e d   s t a t i c   r e a d o n l y   d o u b l e 	 	 l a b e l H e i g h t   =   1 2 ;  
  
 	 	 p r o t e c t e d   D o c u m e n t 	 	 	 	 	 	 d o c u m e n t ;  
 	 	 p r o t e c t e d   L i s t < i n t > 	 	 	 	 	 	 l a y e r s ;  
 	 	 p r o t e c t e d   L i s t < i n t > 	 	 	 	 	 	 l a y e r s T o R e g e n e r a t e ;  
 	 	 p r o t e c t e d   F r a m e B o x 	 	 	 	 	 	 p a r e n t P a n e l ;  
 	 	 p r o t e c t e d   H S l i d e r 	 	 	 	 	 	 s l i d e r ;  
 	 	 p r o t e c t e d   S c r o l l a b l e 	 	 	 	 	 s c r o l l a b l e ;  
 	 	 p r o t e c t e d   b o o l 	 	 	 	 	 	 	 i s P a n e l S h o w e d ;  
 	 }  
 }  
 