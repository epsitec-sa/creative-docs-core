ÿþu s i n g   E p s i t e c . C o m m o n . W i d g e t s ;  
 u s i n g   E p s i t e c . C o m m o n . S u p p o r t ;  
 u s i n g   E p s i t e c . C o m m o n . D r a w i n g ;  
  
 n a m e s p a c e   E p s i t e c . C o m m o n . D e s i g n e r . P a n e l E d i t o r  
 {  
 	 / / /   < s u m m a r y >  
 	 / / /   D e s c r i p t i o n   d ' u n e   c o t e   p o u r   P a n e l E d i t o r .  
 	 / / /   < / s u m m a r y >  
 	 p u b l i c   c l a s s   D i m e n s i o n  
 	 {  
 	 	 / / 	 T y p e s   p o s s i b l e s   p o u r   l e s   d i f f é r e n t e s   c o t e s .  
 	 	 p u b l i c   e n u m   T y p e  
 	 	 {  
 	 	 	 N o n e ,  
 	 	 	 W i d t h ,  
 	 	 	 H e i g h t ,  
 	 	 	 M a r g i n L e f t ,  
 	 	 	 M a r g i n R i g h t ,  
 	 	 	 M a r g i n B o t t o m ,  
 	 	 	 M a r g i n T o p ,  
 	 	 	 P a d d i n g L e f t ,  
 	 	 	 P a d d i n g R i g h t ,  
 	 	 	 P a d d i n g B o t t o m ,  
 	 	 	 P a d d i n g T o p ,  
 	 	 	 G r i d C o l u m n ,  
 	 	 	 G r i d C o l u m n A d d B e f o r e ,  
 	 	 	 G r i d C o l u m n A d d A f t e r ,  
 	 	 	 G r i d C o l u m n R e m o v e ,  
 	 	 	 G r i d R o w ,  
 	 	 	 G r i d R o w A d d B e f o r e ,  
 	 	 	 G r i d R o w A d d A f t e r ,  
 	 	 	 G r i d R o w R e m o v e ,  
 	 	 	 G r i d W i d t h ,  
 	 	 	 G r i d H e i g h t ,  
 	 	 	 G r i d W i d t h M o d e ,  
 	 	 	 G r i d H e i g h t M o d e ,  
 	 	 	 G r i d M a r g i n L e f t ,  
 	 	 	 G r i d M a r g i n R i g h t ,  
 	 	 	 G r i d M a r g i n B o t t o m ,  
 	 	 	 G r i d M a r g i n T o p ,  
 	 	 	 G r i d C o l u m n S p a n I n c ,  
 	 	 	 G r i d C o l u m n S p a n D e c ,  
 	 	 	 G r i d R o w S p a n I n c ,  
 	 	 	 G r i d R o w S p a n D e c ,  
 	 	 	 C h i l d r e n P l a c e m e n t ,  
 	 	 }  
  
  
 	 	 p u b l i c   D i m e n s i o n ( E d i t o r   e d i t o r ,   W i d g e t   o b j ,   T y p e   t y p e )  
 	 	 {  
 	 	 	 / / 	 C r é e   u n e   c o t e .  
 	 	 	 t h i s . e d i t o r   =   e d i t o r ;  
 	 	 	 t h i s . o b j e c t M o d i f i e r   =   e d i t o r . O b j e c t M o d i f i e r ;  
 	 	 	 t h i s . c o n t e x t   =   e d i t o r . C o n t e x t ;  
  
 	 	 	 t h i s . o b j   =   o b j ;  
 	 	 	 t h i s . t y p e   =   t y p e ;  
 	 	 	 t h i s . c o l u m n O r R o w   =   - 1 ;  
 	 	 	 t h i s . s l a v e   =   f a l s e ;  
 	 	 }  
  
 	 	 p u b l i c   D i m e n s i o n ( E d i t o r   e d i t o r ,   W i d g e t   o b j ,   T y p e   t y p e ,   i n t   c o l u m n O r R o w )  
 	 	 {  
 	 	 	 / / 	 C r é e   u n e   c o t e .  
 	 	 	 t h i s . e d i t o r   =   e d i t o r ;  
 	 	 	 t h i s . o b j e c t M o d i f i e r   =   e d i t o r . O b j e c t M o d i f i e r ;  
 	 	 	 t h i s . c o n t e x t   =   e d i t o r . C o n t e x t ;  
  
 	 	 	 t h i s . o b j   =   o b j ;  
 	 	 	 t h i s . t y p e   =   t y p e ;  
 	 	 	 t h i s . c o l u m n O r R o w   =   c o l u m n O r R o w ;  
 	 	 	 t h i s . s l a v e   =   f a l s e ;  
 	 	 }  
  
  
 	 	 p u b l i c   W i d g e t   O b j e c t  
 	 	 {  
 	 	 	 / / 	 R e t o u r n e   l ' o b j e t   c o t é .  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . o b j ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p u b l i c   T y p e   D i m e n s i o n T y p e  
 	 	 {  
 	 	 	 / / 	 R e t o u r n e   l e   t y p e   d ' u n e   c o t e .  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . t y p e ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p u b l i c   i n t   C o l u m n O r R o w  
 	 	 {  
 	 	 	 / / 	 R e t o u r n e   l e   r a n g   d e   l a   l i g n e   o u   d e   l a   c o l o n n e   ( s e l o n   l e   t y p e ) .  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . c o l u m n O r R o w ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p u b l i c   b o o l   S l a v e  
 	 	 {  
 	 	 	 / / 	 E t a t   d e   l a   c o t e .   L o r s q u e   p l u s i e u r s   c o l o n n e s   d ' u n   t a b l e a u   s o n t  
 	 	 	 / / 	 s é l e c t i o n n é e s ,   s e u l e   l a   p r e m i è r e   e s t   m a î t r e ;   t o u t e s   l e s   s u i v a n t e s  
 	 	 	 / / 	 s o n t   e s c l a v e s   ( S l a v e   =   t r u e ) .  
 	 	 	 / / 	 U n e   c o t e   e s c l a v e   n ' e s t   n i   a f f i c h é e   n i   d é t e c t é e .   I l   e s t   s e u l e m e n t  
 	 	 	 / / 	 p o s s i b l e   d e   c h a n g e r   s a   v a l e u r   ( a v e c   l a   p r o p r i é t é   V a l u e ) .  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . s l a v e ;  
 	 	 	 }  
 	 	 	 s e t  
 	 	 	 {  
 	 	 	 	 t h i s . s l a v e   =   v a l u e ;  
 	 	 	 }  
 	 	 }  
  
  
 	 	 p u b l i c   R e c t a n g l e   G e t B o u n d s ( b o o l   i s H i l i t e d )  
 	 	 {  
 	 	 	 / / 	 R e t o u r n e   l e   r e c t a n g l e   e n g l o b a n t   l a   c o t e .  
 	 	 	 P a t h   p a t h   =   t h i s . G e o m e t r y B a c k g r o u n d ( n u l l ) ;  
 	 	 	 R e c t a n g l e   b o u n d s   =   p a t h . C o m p u t e B o u n d s ( ) ;  
  
 	 	 	 i f   ( i s H i l i t e d )  
 	 	 	 {  
 	 	 	 	 p a t h   =   t h i s . G e o m e t r y H i l i t e d S u r f a c e ( n u l l ) ;  
 	 	 	 	 b o u n d s   =   R e c t a n g l e . U n i o n ( b o u n d s ,   p a t h . C o m p u t e B o u n d s ( ) ) ;  
 	 	 	 }  
  
 	 	 	 b o u n d s . I n f l a t e ( 1 ) ;     / /   à   c a u s e   d e s   d i f f é r e n t s   M i s c . A l i g n F o r L i n e  
 	 	 	 r e t u r n   b o u n d s ;  
 	 	 }  
  
  
 	 	 p u b l i c   v o i d   D r a w B a c k g r o u n d ( G r a p h i c s   g r a p h i c s )  
 	 	 {  
 	 	 	 / / 	 D e s s i n e   u n e   c o t e .  
 	 	 	 i f   ( t h i s . s l a v e )  
 	 	 	 {  
 	 	 	 	 r e t u r n ;  
 	 	 	 }  
  
 	 	 	 C o l o r   b o r d e r   =   C o l o r . F r o m B r i g h t n e s s ( 0 . 5 ) ;     / /   g r i s  
  
 	 	 	 P a t h   p a t h   =   t h i s . G e o m e t r y B a c k g r o u n d ( g r a p h i c s ) ;  
 	 	 	 i f   ( p a t h   ! =   n u l l )  
 	 	 	 {  
 	 	 	 	 g r a p h i c s . R a s t e r i z e r . A d d S u r f a c e ( p a t h ) ;  
 	 	 	 	 g r a p h i c s . R e n d e r S o l i d ( t h i s . B a c k g r o u n d C o l o r ) ;  
  
 	 	 	 	 g r a p h i c s . R a s t e r i z e r . A d d O u t l i n e ( p a t h ) ;  
 	 	 	 	 g r a p h i c s . R e n d e r S o l i d ( b o r d e r ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . D r a w O u t l i n e ( g r a p h i c s ,   b o r d e r ) ;  
  
 	 	 	 R e c t a n g l e   b o x   =   t h i s . G e o m e t r y T e x t B o x ;  
 	 	 	 M i s c . A l i g n F o r L i n e ( g r a p h i c s ,   r e f   b o x ) ;  
 	 	 	 t h i s . D r a w T e x t ( g r a p h i c s ,   b o x ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   D r a w O u t l i n e ( G r a p h i c s   g r a p h i c s ,   C o l o r   c o l o r )  
 	 	 {  
 	 	 	 / / 	 D e s s i n e   l e s   t r a i t s   s u p p l é m e n t a i r e s   e t   l e s   r e s s o r t s   d ' u n e   c o t e .  
 	 	 	 R e c t a n g l e   b o u n d s   =   t h i s . o b j e c t M o d i f i e r . G e t A c t u a l B o u n d s ( t h i s . o b j ) ;  
 	 	 	 M a r g i n s   m a r g i n s   =   t h i s . o b j e c t M o d i f i e r . G e t M a r g i n s ( t h i s . o b j ) ;  
 	 	 	 R e c t a n g l e   e x t   =   b o u n d s ;  
 	 	 	 e x t . I n f l a t e ( t h i s . o b j e c t M o d i f i e r . G e t M a r g i n s ( t h i s . o b j ) ) ;  
 	 	 	 M a r g i n s   p a d d i n g   =   t h i s . o b j e c t M o d i f i e r . G e t P a d d i n g ( t h i s . o b j ) ;  
 	 	 	 R e c t a n g l e   i n s i d e   =   t h i s . o b j e c t M o d i f i e r . G e t F i n a l P a d d i n g ( t h i s . o b j ) ;  
  
 	 	 	 R e c t a n g l e   r ,   b o x ;  
 	 	 	 P a t h   p a t h ;  
 	 	 	 P o i n t   p 1 ,   p 2 ;  
 	 	 	 d o u b l e   v a l u e ;  
  
 	 	 	 b o x   =   t h i s . G e o m e t r y T e x t B o x ;  
 	 	 	 M i s c . A l i g n F o r L i n e ( g r a p h i c s ,   r e f   b o x ) ;  
  
 	 	 	 s w i t c h   ( t h i s . t y p e )  
 	 	 	 {  
 	 	 	 	 c a s e   T y p e . W i d t h :  
 	 	 	 	 	 v a l u e   =   t h i s . V a l u e ;  
 	 	 	 	 	 i f   ( v a l u e   ! =   b o u n d s . W i d t h )     / /   f o r m e   c o m p l e x e   ?  
 	 	 	 	 	 {  
 	 	 	 	 	 	 r   =   b o x ;  
 	 	 	 	 	 	 d o u b l e   h a l f   =   S y s t e m . M a t h . M a x ( S y s t e m . M a t h . F l o o r ( v a l u e / 2 ) ,   5 ) ;  
 	 	 	 	 	 	 d o u b l e   m i d d l e   =   S y s t e m . M a t h . F l o o r ( r . C e n t e r . X ) + 0 . 5 ;  
 	 	 	 	 	 	 r . L e f t   =   m i d d l e - h a l f ;  
 	 	 	 	 	 	 r . W i d t h   =   h a l f * 2 ;  
 	 	 	 	 	 	 h a l f   =   S y s t e m . M a t h . F l o o r ( v a l u e / 2 ) ;  
 	 	 	 	 	 	 p 1   =   n e w   P o i n t ( m i d d l e - h a l f ,   e x t . B o t t o m ) ;  
 	 	 	 	 	 	 p 2   =   n e w   P o i n t ( p 1 . X + h a l f * 2 ,   e x t . B o t t o m ) ;  
 	 	 	 	 	 	 p a t h   =   n e w   P a t h ( ) ;  
 	 	 	 	 	 	 p a t h . M o v e T o ( p 1 ) ;  
 	 	 	 	 	 	 p a t h . L i n e T o ( r . T o p L e f t ) ;  
 	 	 	 	 	 	 p a t h . L i n e T o ( r . B o t t o m L e f t ) ;  
 	 	 	 	 	 	 p a t h . L i n e T o ( r . B o t t o m R i g h t ) ;  
 	 	 	 	 	 	 p a t h . L i n e T o ( r . T o p R i g h t ) ;  
 	 	 	 	 	 	 p a t h . L i n e T o ( p 2 ) ;  
 	 	 	 	 	 	 p a t h . C l o s e ( ) ;  
 	 	 	 	 	 	 g r a p h i c s . R a s t e r i z e r . A d d O u t l i n e ( p a t h ) ;  
 	 	 	 	 	 	 g r a p h i c s . R e n d e r S o l i d ( c o l o r ) ;  
  
 	 	 	 	 	 	 t h i s . D r a w S p r i n g ( g r a p h i c s ,   n e w   P o i n t ( b o x . L e f t ,   b o x . C e n t e r . Y ) ,   n e w   P o i n t ( r . L e f t ,   b o x . C e n t e r . Y ) ,   c o l o r ) ;  
 	 	 	 	 	 	 t h i s . D r a w S p r i n g ( g r a p h i c s ,   n e w   P o i n t ( b o x . R i g h t ,   b o x . C e n t e r . Y ) ,   n e w   P o i n t ( r . R i g h t ,   b o x . C e n t e r . Y ) ,   c o l o r ) ;  
 	 	 	 	 	 }  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . H e i g h t :  
 	 	 	 	 	 v a l u e   =   t h i s . V a l u e ;  
 	 	 	 	 	 i f   ( v a l u e   ! =   b o u n d s . H e i g h t )     / /   f o r m e   c o m p l e x e   ?  
 	 	 	 	 	 {  
 	 	 	 	 	 	 r   =   b o x ;  
 	 	 	 	 	 	 d o u b l e   h a l f   =   S y s t e m . M a t h . M a x ( S y s t e m . M a t h . F l o o r ( v a l u e / 2 ) ,   5 ) ;  
 	 	 	 	 	 	 d o u b l e   m i d d l e   =   S y s t e m . M a t h . F l o o r ( r . C e n t e r . Y ) + 0 . 5 ;  
 	 	 	 	 	 	 r . B o t t o m   =   m i d d l e - h a l f ;  
 	 	 	 	 	 	 r . H e i g h t   =   h a l f * 2 ;  
 	 	 	 	 	 	 h a l f   =   S y s t e m . M a t h . F l o o r ( v a l u e / 2 ) ;  
 	 	 	 	 	 	 p 1   =   n e w   P o i n t ( e x t . R i g h t ,   m i d d l e - h a l f ) ;  
 	 	 	 	 	 	 p 2   =   n e w   P o i n t ( e x t . R i g h t ,   p 1 . Y + h a l f * 2 ) ;  
 	 	 	 	 	 	 p a t h   =   n e w   P a t h ( ) ;  
 	 	 	 	 	 	 p a t h . M o v e T o ( p 1 ) ;  
 	 	 	 	 	 	 p a t h . L i n e T o ( r . B o t t o m L e f t ) ;  
 	 	 	 	 	 	 p a t h . L i n e T o ( r . B o t t o m R i g h t ) ;  
 	 	 	 	 	 	 p a t h . L i n e T o ( r . T o p R i g h t ) ;  
 	 	 	 	 	 	 p a t h . L i n e T o ( r . T o p L e f t ) ;  
 	 	 	 	 	 	 p a t h . L i n e T o ( p 2 ) ;  
 	 	 	 	 	 	 p a t h . C l o s e ( ) ;  
 	 	 	 	 	 	 g r a p h i c s . R a s t e r i z e r . A d d O u t l i n e ( p a t h ) ;  
 	 	 	 	 	 	 g r a p h i c s . R e n d e r S o l i d ( c o l o r ) ;  
  
 	 	 	 	 	 	 t h i s . D r a w S p r i n g ( g r a p h i c s ,   n e w   P o i n t ( b o x . C e n t e r . X ,   b o x . B o t t o m ) ,   n e w   P o i n t ( b o x . C e n t e r . X ,   r . B o t t o m ) ,   c o l o r ) ;  
 	 	 	 	 	 	 t h i s . D r a w S p r i n g ( g r a p h i c s ,   n e w   P o i n t ( b o x . C e n t e r . X ,   b o x . T o p ) ,   n e w   P o i n t ( b o x . C e n t e r . X ,   r . T o p ) ,   c o l o r ) ;  
 	 	 	 	 	 }  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . G r i d W i d t h :  
 	 	 	 	 	 v a l u e   =   t h i s . V a l u e ;  
 	 	 	 	 	 i f   ( v a l u e   ! =   b o x . W i d t h   & &   ! t h i s . I s P e r c e n t )     / /   f o r m e   c o m p l e x e   ?  
 	 	 	 	 	 {  
 	 	 	 	 	 	 r   =   b o x ;  
 	 	 	 	 	 	 d o u b l e   h a l f   =   S y s t e m . M a t h . M a x ( S y s t e m . M a t h . F l o o r ( v a l u e / 2 ) ,   5 ) ;  
 	 	 	 	 	 	 d o u b l e   m i d d l e   =   S y s t e m . M a t h . F l o o r ( r . C e n t e r . X ) + 0 . 5 ;  
 	 	 	 	 	 	 r . L e f t   =   m i d d l e - h a l f ;  
 	 	 	 	 	 	 r . W i d t h   =   h a l f * 2 ;  
 	 	 	 	 	 	 h a l f   =   S y s t e m . M a t h . F l o o r ( v a l u e / 2 ) ;  
 	 	 	 	 	 	 p 1   =   n e w   P o i n t ( m i d d l e - h a l f ,   i n s i d e . T o p ) ;  
 	 	 	 	 	 	 p 2   =   n e w   P o i n t ( p 1 . X + h a l f * 2 ,   i n s i d e . T o p ) ;  
 	 	 	 	 	 	 p a t h   =   n e w   P a t h ( ) ;  
 	 	 	 	 	 	 p a t h . M o v e T o ( p 1 ) ;  
 	 	 	 	 	 	 p a t h . L i n e T o ( r . B o t t o m L e f t ) ;  
 	 	 	 	 	 	 p a t h . L i n e T o ( r . T o p L e f t ) ;  
 	 	 	 	 	 	 p a t h . L i n e T o ( r . T o p R i g h t ) ;  
 	 	 	 	 	 	 p a t h . L i n e T o ( r . B o t t o m R i g h t ) ;  
 	 	 	 	 	 	 p a t h . L i n e T o ( p 2 ) ;  
 	 	 	 	 	 	 p a t h . C l o s e ( ) ;  
 	 	 	 	 	 	 g r a p h i c s . R a s t e r i z e r . A d d O u t l i n e ( p a t h ) ;  
 	 	 	 	 	 	 g r a p h i c s . R e n d e r S o l i d ( c o l o r ) ;  
  
 	 	 	 	 	 	 t h i s . D r a w S p r i n g ( g r a p h i c s ,   n e w   P o i n t ( b o x . L e f t ,   b o x . C e n t e r . Y ) ,   n e w   P o i n t ( r . L e f t ,   b o x . C e n t e r . Y ) ,   c o l o r ) ;  
 	 	 	 	 	 	 t h i s . D r a w S p r i n g ( g r a p h i c s ,   n e w   P o i n t ( b o x . R i g h t ,   b o x . C e n t e r . Y ) ,   n e w   P o i n t ( r . R i g h t ,   b o x . C e n t e r . Y ) ,   c o l o r ) ;  
 	 	 	 	 	 }  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . G r i d H e i g h t :  
 	 	 	 	 	 v a l u e   =   t h i s . V a l u e ;  
 	 	 	 	 	 i f   ( v a l u e   ! =   b o x . H e i g h t   & &   ! t h i s . I s P e r c e n t )     / /   f o r m e   c o m p l e x e   ?  
 	 	 	 	 	 {  
 	 	 	 	 	 	 r   =   b o x ;  
 	 	 	 	 	 	 d o u b l e   h a l f   =   S y s t e m . M a t h . M a x ( S y s t e m . M a t h . F l o o r ( v a l u e / 2 ) ,   5 ) ;  
 	 	 	 	 	 	 d o u b l e   m i d d l e   =   S y s t e m . M a t h . F l o o r ( r . C e n t e r . Y ) + 0 . 5 ;  
 	 	 	 	 	 	 r . B o t t o m   =   m i d d l e - h a l f ;  
 	 	 	 	 	 	 r . H e i g h t   =   h a l f * 2 ;  
 	 	 	 	 	 	 h a l f   =   S y s t e m . M a t h . F l o o r ( v a l u e / 2 ) ;  
 	 	 	 	 	 	 p 1   =   n e w   P o i n t ( i n s i d e . L e f t ,   m i d d l e - h a l f ) ;  
 	 	 	 	 	 	 p 2   =   n e w   P o i n t ( i n s i d e . L e f t ,   p 1 . Y + h a l f * 2 ) ;  
 	 	 	 	 	 	 p a t h   =   n e w   P a t h ( ) ;  
 	 	 	 	 	 	 p a t h . M o v e T o ( p 1 ) ;  
 	 	 	 	 	 	 p a t h . L i n e T o ( r . B o t t o m R i g h t ) ;  
 	 	 	 	 	 	 p a t h . L i n e T o ( r . B o t t o m L e f t ) ;  
 	 	 	 	 	 	 p a t h . L i n e T o ( r . T o p L e f t ) ;  
 	 	 	 	 	 	 p a t h . L i n e T o ( r . T o p R i g h t ) ;  
 	 	 	 	 	 	 p a t h . L i n e T o ( p 2 ) ;  
 	 	 	 	 	 	 p a t h . C l o s e ( ) ;  
 	 	 	 	 	 	 g r a p h i c s . R a s t e r i z e r . A d d O u t l i n e ( p a t h ) ;  
 	 	 	 	 	 	 g r a p h i c s . R e n d e r S o l i d ( c o l o r ) ;  
  
 	 	 	 	 	 	 t h i s . D r a w S p r i n g ( g r a p h i c s ,   n e w   P o i n t ( b o x . C e n t e r . X ,   b o x . B o t t o m ) ,   n e w   P o i n t ( b o x . C e n t e r . X ,   r . B o t t o m ) ,   c o l o r ) ;  
 	 	 	 	 	 	 t h i s . D r a w S p r i n g ( g r a p h i c s ,   n e w   P o i n t ( b o x . C e n t e r . X ,   b o x . T o p ) ,   n e w   P o i n t ( b o x . C e n t e r . X ,   r . T o p ) ,   c o l o r ) ;  
 	 	 	 	 	 }  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . G r i d C o l u m n :  
 	 	 	 	 	 g r a p h i c s . A d d L i n e ( b o x . L e f t ,   e x t . T o p ,   b o x . L e f t ,   i n s i d e . T o p ) ;  
 	 	 	 	 	 g r a p h i c s . A d d L i n e ( b o x . R i g h t ,   e x t . T o p ,   b o x . R i g h t ,   i n s i d e . T o p ) ;  
 	 	 	 	 	 g r a p h i c s . R e n d e r S o l i d ( c o l o r ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . G r i d M a r g i n L e f t :  
 	 	 	 	 	 g r a p h i c s . A d d L i n e ( b o x . R i g h t ,   e x t . T o p ,   b o x . R i g h t ,   i n s i d e . T o p ) ;  
 	 	 	 	 	 g r a p h i c s . R e n d e r S o l i d ( c o l o r ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . G r i d M a r g i n R i g h t :  
 	 	 	 	 	 g r a p h i c s . A d d L i n e ( b o x . L e f t ,   e x t . T o p ,   b o x . L e f t ,   i n s i d e . T o p ) ;  
 	 	 	 	 	 g r a p h i c s . R e n d e r S o l i d ( c o l o r ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . G r i d R o w :  
 	 	 	 	 	 g r a p h i c s . A d d L i n e ( e x t . L e f t ,   b o x . T o p ,   i n s i d e . L e f t ,   b o x . T o p ) ;  
 	 	 	 	 	 g r a p h i c s . A d d L i n e ( e x t . L e f t ,   b o x . B o t t o m ,   i n s i d e . L e f t ,   b o x . B o t t o m ) ;  
 	 	 	 	 	 g r a p h i c s . R e n d e r S o l i d ( c o l o r ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . G r i d M a r g i n T o p :  
 	 	 	 	 	 g r a p h i c s . A d d L i n e ( e x t . L e f t ,   b o x . B o t t o m ,   i n s i d e . L e f t ,   b o x . B o t t o m ) ;  
 	 	 	 	 	 g r a p h i c s . R e n d e r S o l i d ( c o l o r ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . G r i d M a r g i n B o t t o m :  
 	 	 	 	 	 g r a p h i c s . A d d L i n e ( e x t . L e f t ,   b o x . T o p ,   i n s i d e . L e f t ,   b o x . T o p ) ;  
 	 	 	 	 	 g r a p h i c s . R e n d e r S o l i d ( c o l o r ) ;  
 	 	 	 	 	 b r e a k ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p u b l i c   v o i d   D r a w D i m e n s i o n ( G r a p h i c s   g r a p h i c s )  
 	 	 {  
 	 	 	 / / 	 D e s s i n e   l a   m a r q u e   d e   l o n g u e u r   d ' u n e   c o t e .  
 	 	 	 R e c t a n g l e   b o u n d s   =   t h i s . o b j e c t M o d i f i e r . G e t A c t u a l B o u n d s ( t h i s . o b j ) ;  
 	 	 	 M a r g i n s   m a r g i n s   =   t h i s . o b j e c t M o d i f i e r . G e t M a r g i n s ( t h i s . o b j ) ;  
 	 	 	 R e c t a n g l e   e x t   =   b o u n d s ;  
 	 	 	 e x t . I n f l a t e ( t h i s . o b j e c t M o d i f i e r . G e t M a r g i n s ( t h i s . o b j ) ) ;  
 	 	 	 R e c t a n g l e   i n s i d e   =   t h i s . o b j e c t M o d i f i e r . G e t F i n a l P a d d i n g ( t h i s . o b j ) ;  
  
 	 	 	 R e c t a n g l e   r ,   b o x ;  
 	 	 	 P o i n t   p 1 ,   p 2 ;  
 	 	 	 d o u b l e   v a l u e ;  
  
 	 	 	 b o x   =   t h i s . G e o m e t r y T e x t B o x ;  
 	 	 	 M i s c . A l i g n F o r L i n e ( g r a p h i c s ,   r e f   b o x ) ;  
  
 	 	 	 s w i t c h   ( t h i s . t y p e )  
 	 	 	 {  
 	 	 	 	 c a s e   T y p e . W i d t h :  
 	 	 	 	 	 r   =   b o x ;  
 	 	 	 	 	 r . T o p   =   e x t . B o t t o m ;  
  
 	 	 	 	 	 v a l u e   =   t h i s . V a l u e ;  
 	 	 	 	 	 i f   ( v a l u e   = =   b o u n d s . W i d t h )     / /   f o r m e   r e c t a n g u l a i r e   s i m p l e   ?  
 	 	 	 	 	 {  
 	 	 	 	 	 	 p 1   =   n e w   P o i n t ( b o x . R i g h t ,   e x t . B o t t o m ) ;  
 	 	 	 	 	 	 p 2   =   n e w   P o i n t ( b o x . L e f t ,   e x t . B o t t o m ) ;  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e  
 	 	 	 	 	 {  
 	 	 	 	 	 	 d o u b l e   h a l f   =   S y s t e m . M a t h . F l o o r ( v a l u e / 2 ) ;  
 	 	 	 	 	 	 d o u b l e   m i d d l e   =   S y s t e m . M a t h . F l o o r ( r . C e n t e r . X ) + 0 . 5 ;  
 	 	 	 	 	 	 p 1   =   n e w   P o i n t ( m i d d l e - h a l f ,   e x t . B o t t o m ) ;  
 	 	 	 	 	 	 p 2   =   n e w   P o i n t ( p 1 . X + h a l f * 2 ,   e x t . B o t t o m ) ;  
 	 	 	 	 	 }  
 	 	 	 	 	 t h i s . D r a w L i n e ( g r a p h i c s ,   p 1 ,   p 2 ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . H e i g h t :  
 	 	 	 	 	 r   =   b o x ;  
 	 	 	 	 	 r . L e f t   =   e x t . R i g h t ;  
  
 	 	 	 	 	 v a l u e   =   t h i s . V a l u e ;  
 	 	 	 	 	 i f   ( v a l u e   = =   b o u n d s . H e i g h t )     / /   f o r m e   r e c t a n g u l a i r e   s i m p l e   ?  
 	 	 	 	 	 {  
 	 	 	 	 	 	 p 1   =   n e w   P o i n t ( e x t . R i g h t ,   b o x . T o p ) ;  
 	 	 	 	 	 	 p 2   =   n e w   P o i n t ( e x t . R i g h t ,   b o x . B o t t o m ) ;  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e  
 	 	 	 	 	 {  
 	 	 	 	 	 	 d o u b l e   h a l f   =   S y s t e m . M a t h . F l o o r ( v a l u e / 2 ) ;  
 	 	 	 	 	 	 d o u b l e   m i d d l e   =   S y s t e m . M a t h . F l o o r ( r . C e n t e r . Y ) + 0 . 5 ;  
 	 	 	 	 	 	 p 1   =   n e w   P o i n t ( e x t . R i g h t ,   m i d d l e - h a l f ) ;  
 	 	 	 	 	 	 p 2   =   n e w   P o i n t ( e x t . R i g h t ,   p 1 . Y + h a l f * 2 ) ;  
 	 	 	 	 	 }  
 	 	 	 	 	 t h i s . D r a w L i n e ( g r a p h i c s ,   p 1 ,   p 2 ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . M a r g i n L e f t :  
 	 	 	 	 	 p 2   =   n e w   P o i n t ( b o x . R i g h t ,   e x t . B o t t o m ) ;  
 	 	 	 	 	 p 1   =   n e w   P o i n t ( b o x . R i g h t - m a r g i n s . L e f t ,   e x t . B o t t o m ) ;  
 	 	 	 	 	 t h i s . D r a w L i n e ( g r a p h i c s ,   p 1 ,   p 2 ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . M a r g i n R i g h t :  
 	 	 	 	 	 p 1   =   n e w   P o i n t ( b o x . L e f t ,   e x t . B o t t o m ) ;  
 	 	 	 	 	 p 2   =   n e w   P o i n t ( b o x . L e f t + m a r g i n s . R i g h t ,   e x t . B o t t o m ) ;  
 	 	 	 	 	 t h i s . D r a w L i n e ( g r a p h i c s ,   p 1 ,   p 2 ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . M a r g i n B o t t o m :  
 	 	 	 	 	 p 2   =   n e w   P o i n t ( e x t . R i g h t ,   b o x . T o p ) ;  
 	 	 	 	 	 p 1   =   n e w   P o i n t ( e x t . R i g h t ,   b o x . T o p - m a r g i n s . B o t t o m ) ;  
 	 	 	 	 	 t h i s . D r a w L i n e ( g r a p h i c s ,   p 1 ,   p 2 ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . M a r g i n T o p :  
 	 	 	 	 	 p 1   =   n e w   P o i n t ( e x t . R i g h t ,   b o x . B o t t o m ) ;  
 	 	 	 	 	 p 2   =   n e w   P o i n t ( e x t . R i g h t ,   b o x . B o t t o m + m a r g i n s . T o p ) ;  
 	 	 	 	 	 t h i s . D r a w L i n e ( g r a p h i c s ,   p 1 ,   p 2 ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . G r i d W i d t h :  
 	 	 	 	 	 i f   ( ! t h i s . I s P e r c e n t )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 r   =   b o x ;  
 	 	 	 	 	 	 r . B o t t o m   =   i n s i d e . T o p ;  
  
 	 	 	 	 	 	 v a l u e   =   t h i s . V a l u e ;  
 	 	 	 	 	 	 i f   ( v a l u e   = =   b o x . W i d t h )     / /   f o r m e   r e c t a n g u l a i r e   s i m p l e   ?  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 p 1   =   n e w   P o i n t ( b o x . R i g h t ,   i n s i d e . T o p ) ;  
 	 	 	 	 	 	 	 p 2   =   n e w   P o i n t ( b o x . L e f t ,   i n s i d e . T o p ) ;  
 	 	 	 	 	 	 }  
 	 	 	 	 	 	 e l s e  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 d o u b l e   h a l f   =   S y s t e m . M a t h . F l o o r ( v a l u e / 2 ) ;  
 	 	 	 	 	 	 	 d o u b l e   m i d d l e   =   S y s t e m . M a t h . F l o o r ( r . C e n t e r . X ) + 0 . 5 ;  
 	 	 	 	 	 	 	 p 1   =   n e w   P o i n t ( m i d d l e - h a l f ,   i n s i d e . T o p ) ;  
 	 	 	 	 	 	 	 p 2   =   n e w   P o i n t ( p 1 . X + h a l f * 2 ,   i n s i d e . T o p ) ;  
 	 	 	 	 	 	 }  
 	 	 	 	 	 	 t h i s . D r a w L i n e ( g r a p h i c s ,   p 1 ,   p 2 ) ;  
 	 	 	 	 	 }  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . G r i d H e i g h t :  
 	 	 	 	 	 i f   ( ! t h i s . I s P e r c e n t )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 r   =   b o x ;  
 	 	 	 	 	 	 r . R i g h t   =   i n s i d e . L e f t ;  
  
 	 	 	 	 	 	 v a l u e   =   t h i s . V a l u e ;  
 	 	 	 	 	 	 i f   ( v a l u e   = =   b o u n d s . H e i g h t )     / /   f o r m e   r e c t a n g u l a i r e   s i m p l e   ?  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 p 1   =   n e w   P o i n t ( i n s i d e . L e f t ,   b o x . T o p ) ;  
 	 	 	 	 	 	 	 p 2   =   n e w   P o i n t ( i n s i d e . L e f t ,   b o x . B o t t o m ) ;  
 	 	 	 	 	 	 }  
 	 	 	 	 	 	 e l s e  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 d o u b l e   h a l f   =   S y s t e m . M a t h . F l o o r ( v a l u e / 2 ) ;  
 	 	 	 	 	 	 	 d o u b l e   m i d d l e   =   S y s t e m . M a t h . F l o o r ( r . C e n t e r . Y ) + 0 . 5 ;  
 	 	 	 	 	 	 	 p 1   =   n e w   P o i n t ( i n s i d e . L e f t ,   m i d d l e - h a l f ) ;  
 	 	 	 	 	 	 	 p 2   =   n e w   P o i n t ( i n s i d e . L e f t ,   p 1 . Y + h a l f * 2 ) ;  
 	 	 	 	 	 	 }  
 	 	 	 	 	 	 t h i s . D r a w L i n e ( g r a p h i c s ,   p 1 ,   p 2 ) ;  
 	 	 	 	 	 }  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . G r i d M a r g i n L e f t :  
 	 	 	 	 	 p 2   =   n e w   P o i n t ( b o x . R i g h t ,   i n s i d e . T o p ) ;  
 	 	 	 	 	 p 1   =   n e w   P o i n t ( b o x . R i g h t - t h i s . V a l u e ,   i n s i d e . T o p ) ;  
 	 	 	 	 	 t h i s . D r a w L i n e ( g r a p h i c s ,   p 1 ,   p 2 ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . G r i d M a r g i n R i g h t :  
 	 	 	 	 	 p 1   =   n e w   P o i n t ( b o x . L e f t ,   i n s i d e . T o p ) ;  
 	 	 	 	 	 p 2   =   n e w   P o i n t ( b o x . L e f t + t h i s . V a l u e ,   i n s i d e . T o p ) ;  
 	 	 	 	 	 t h i s . D r a w L i n e ( g r a p h i c s ,   p 1 ,   p 2 ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . G r i d M a r g i n B o t t o m :  
 	 	 	 	 	 p 2   =   n e w   P o i n t ( i n s i d e . L e f t ,   b o x . T o p ) ;  
 	 	 	 	 	 p 1   =   n e w   P o i n t ( i n s i d e . L e f t ,   b o x . T o p - t h i s . V a l u e ) ;  
 	 	 	 	 	 t h i s . D r a w L i n e ( g r a p h i c s ,   p 1 ,   p 2 ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . G r i d M a r g i n T o p :  
 	 	 	 	 	 p 1   =   n e w   P o i n t ( i n s i d e . L e f t ,   b o x . B o t t o m ) ;  
 	 	 	 	 	 p 2   =   n e w   P o i n t ( i n s i d e . L e f t ,   b o x . B o t t o m + t h i s . V a l u e ) ;  
 	 	 	 	 	 t h i s . D r a w L i n e ( g r a p h i c s ,   p 1 ,   p 2 ) ;  
 	 	 	 	 	 b r e a k ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p u b l i c   v o i d   D r a w H i l i t e ( G r a p h i c s   g r a p h i c s ,   b o o l   d a r k )  
 	 	 {  
 	 	 	 / / 	 D e s s i n e   l a   c o t e   s u r v o l é e   p a r   l a   s o u r i s .  
 	 	 	 i f   ( t h i s . s l a v e )  
 	 	 	 {  
 	 	 	 	 r e t u r n ;  
 	 	 	 }  
  
 	 	 	 R e c t a n g l e   b o u n d s   =   t h i s . o b j e c t M o d i f i e r . G e t A c t u a l B o u n d s ( t h i s . o b j ) ;  
 	 	 	 d o u b l e   a l p h a   =   d a r k   ?   1 . 0   :   0 . 5 ;  
 	 	 	 C o l o r   h i l i t e   =   C o l o r . F r o m A l p h a R g b ( a l p h a ,   2 5 5 . 0 / 2 5 5 . 0 ,   1 2 4 . 0 / 2 5 5 . 0 ,   3 7 . 0 / 2 5 5 . 0 ) ;  
 	 	 	 C o l o r   b o r d e r   =   C o l o r . F r o m B r i g h t n e s s ( 0 ) ;  
  
 	 	 	 R e c t a n g l e   b o x   =   t h i s . G e o m e t r y T e x t B o x ;  
 	 	 	 M i s c . A l i g n F o r L i n e ( g r a p h i c s ,   r e f   b o x ) ;  
  
 	 	 	 / / 	 D e s s i n e   l a   f o r m e   a v e c   l e s   b o s s e s   u p / d o w n .  
 	 	 	 P a t h   p a t h   =   t h i s . G e o m e t r y H i l i t e d S u r f a c e ( g r a p h i c s ) ;  
 	 	 	 g r a p h i c s . R a s t e r i z e r . A d d S u r f a c e ( p a t h ) ;  
 	 	 	 g r a p h i c s . R e n d e r S o l i d ( h i l i t e ) ;  
 	 	 	 g r a p h i c s . R a s t e r i z e r . A d d O u t l i n e ( p a t h ) ;  
 	 	 	 g r a p h i c s . R e n d e r S o l i d ( b o r d e r ) ;  
  
 	 	 	 t h i s . D r a w O u t l i n e ( g r a p h i c s ,   b o r d e r ) ;     / /   r e d e s s i n e   l e s   r e s s o r t s   p a r   d e s s u s  
 	 	 	 t h i s . D r a w T e x t ( g r a p h i c s ,   b o x ) ;     / /   r e d e s s i n e   l a   v a l e u r   p a r   d e s s u s  
  
 	 	 	 / / 	 D e s s i n e   l e s   s i g n e s   + / - .  
 	 	 	 i f   ( ! t h i s . I s S i m p l e R e c t a n g l e )  
 	 	 	 {  
 	 	 	 	 d o u b l e   t   =   2 0 ;  
  
 	 	 	 	 P o i n t   p   =   n e w   P o i n t ( b o x . C e n t e r . X - 1 ,   b o x . T o p + t / 4 - 2 ) ;  
 	 	 	 	 M i s c . A l i g n F o r L i n e ( g r a p h i c s ,   r e f   p ) ;  
 	 	 	 	 g r a p h i c s . A d d L i n e ( p . X - 2 ,   p . Y ,   p . X + 2 ,   p . Y ) ;  
 	 	 	 	 g r a p h i c s . A d d L i n e ( p . X ,   p . Y - 2 ,   p . X ,   p . Y + 2 ) ;     / /   c r o i x   p o u r   ' + '  
  
 	 	 	 	 p   =   n e w   P o i n t ( b o x . C e n t e r . X - 1 ,   b o x . B o t t o m - t / 4 + 1 ) ;  
 	 	 	 	 M i s c . A l i g n F o r L i n e ( g r a p h i c s ,   r e f   p ) ;  
 	 	 	 	 g r a p h i c s . A d d L i n e ( p . X - 2 ,   p . Y ,   p . X + 2 ,   p . Y ) ;     / /   t r a i t   p o u r   ' - '  
  
 	 	 	 	 g r a p h i c s . R e n d e r S o l i d ( b o r d e r ) ;  
 	 	 	 }  
 	 	 }  
  
  
 	 	 p u b l i c   d o u b l e   V a l u e  
 	 	 {  
 	 	 	 / / 	 V a l e u r   r é e l l e   r e p r é s e n t é e   p a r   l a   c o t e .  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 s w i t c h   ( t h i s . t y p e )  
 	 	 	 	 {  
 	 	 	 	 	 c a s e   T y p e . W i d t h :  
 	 	 	 	 	 	 r e t u r n   t h i s . o b j e c t M o d i f i e r . G e t W i d t h ( t h i s . o b j ) ;  
  
 	 	 	 	 	 c a s e   T y p e . H e i g h t :  
 	 	 	 	 	 	 r e t u r n   t h i s . o b j e c t M o d i f i e r . G e t H e i g h t ( t h i s . o b j ) ;  
  
 	 	 	 	 	 c a s e   T y p e . M a r g i n L e f t :  
 	 	 	 	 	 	 r e t u r n   t h i s . o b j e c t M o d i f i e r . G e t M a r g i n s ( t h i s . o b j ) . L e f t ;  
  
 	 	 	 	 	 c a s e   T y p e . M a r g i n R i g h t :  
 	 	 	 	 	 	 r e t u r n   t h i s . o b j e c t M o d i f i e r . G e t M a r g i n s ( t h i s . o b j ) . R i g h t ;  
  
 	 	 	 	 	 c a s e   T y p e . M a r g i n B o t t o m :  
 	 	 	 	 	 	 r e t u r n   t h i s . o b j e c t M o d i f i e r . G e t M a r g i n s ( t h i s . o b j ) . B o t t o m ;  
  
 	 	 	 	 	 c a s e   T y p e . M a r g i n T o p :  
 	 	 	 	 	 	 r e t u r n   t h i s . o b j e c t M o d i f i e r . G e t M a r g i n s ( t h i s . o b j ) . T o p ;  
  
 	 	 	 	 	 c a s e   T y p e . P a d d i n g L e f t :  
 	 	 	 	 	 	 r e t u r n   t h i s . o b j e c t M o d i f i e r . G e t P a d d i n g ( t h i s . o b j ) . L e f t ;  
  
 	 	 	 	 	 c a s e   T y p e . P a d d i n g R i g h t :  
 	 	 	 	 	 	 r e t u r n   t h i s . o b j e c t M o d i f i e r . G e t P a d d i n g ( t h i s . o b j ) . R i g h t ;  
  
 	 	 	 	 	 c a s e   T y p e . P a d d i n g B o t t o m :  
 	 	 	 	 	 	 r e t u r n   t h i s . o b j e c t M o d i f i e r . G e t P a d d i n g ( t h i s . o b j ) . B o t t o m ;  
  
 	 	 	 	 	 c a s e   T y p e . P a d d i n g T o p :  
 	 	 	 	 	 	 r e t u r n   t h i s . o b j e c t M o d i f i e r . G e t P a d d i n g ( t h i s . o b j ) . T o p ;  
  
 	 	 	 	 	 c a s e   T y p e . G r i d W i d t h :  
 	 	 	 	 	 	 i f   ( t h i s . o b j e c t M o d i f i e r . G e t G r i d C o l u m n M o d e ( t h i s . o b j ,   t h i s . c o l u m n O r R o w )   = =   O b j e c t M o d i f i e r . G r i d M o d e . A u t o )  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 r e t u r n   t h i s . o b j e c t M o d i f i e r . G e t G r i d C o l u m n M i n W i d t h ( t h i s . o b j ,   t h i s . c o l u m n O r R o w ) ;  
 	 	 	 	 	 	 }  
 	 	 	 	 	 	 e l s e  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 r e t u r n   t h i s . o b j e c t M o d i f i e r . G e t G r i d C o l u m n W i d t h ( t h i s . o b j ,   t h i s . c o l u m n O r R o w ) ;  
 	 	 	 	 	 	 }  
  
 	 	 	 	 	 c a s e   T y p e . G r i d H e i g h t :  
 	 	 	 	 	 	 i f   ( t h i s . o b j e c t M o d i f i e r . G e t G r i d R o w M o d e ( t h i s . o b j ,   t h i s . c o l u m n O r R o w )   = =   O b j e c t M o d i f i e r . G r i d M o d e . A u t o )  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 r e t u r n   t h i s . o b j e c t M o d i f i e r . G e t G r i d R o w M i n H e i g h t ( t h i s . o b j ,   t h i s . c o l u m n O r R o w ) ;  
 	 	 	 	 	 	 }  
 	 	 	 	 	 	 e l s e  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 r e t u r n   t h i s . o b j e c t M o d i f i e r . G e t G r i d R o w H e i g h t ( t h i s . o b j ,   t h i s . c o l u m n O r R o w ) ;  
 	 	 	 	 	 	 }  
  
 	 	 	 	 	 c a s e   T y p e . G r i d M a r g i n L e f t :  
 	 	 	 	 	 	 r e t u r n   t h i s . o b j e c t M o d i f i e r . G e t G r i d C o l u m n L e f t B o r d e r ( t h i s . o b j ,   t h i s . c o l u m n O r R o w ) ;  
  
 	 	 	 	 	 c a s e   T y p e . G r i d M a r g i n R i g h t :  
 	 	 	 	 	 	 r e t u r n   t h i s . o b j e c t M o d i f i e r . G e t G r i d C o l u m n R i g h t B o r d e r ( t h i s . o b j ,   t h i s . c o l u m n O r R o w ) ;  
  
 	 	 	 	 	 c a s e   T y p e . G r i d M a r g i n B o t t o m :  
 	 	 	 	 	 	 r e t u r n   t h i s . o b j e c t M o d i f i e r . G e t G r i d R o w B o t t o m B o r d e r ( t h i s . o b j ,   t h i s . c o l u m n O r R o w ) ;  
  
 	 	 	 	 	 c a s e   T y p e . G r i d M a r g i n T o p :  
 	 	 	 	 	 	 r e t u r n   t h i s . o b j e c t M o d i f i e r . G e t G r i d R o w T o p B o r d e r ( t h i s . o b j ,   t h i s . c o l u m n O r R o w ) ;  
  
 	 	 	 	 	 d e f a u l t :  
 	 	 	 	 	 	 r e t u r n   0 ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 s e t  
 	 	 	 {  
 	 	 	 	 M a r g i n s   m ;  
  
 	 	 	 	 s w i t c h   ( t h i s . t y p e )  
 	 	 	 	 {  
 	 	 	 	 	 c a s e   T y p e . W i d t h :  
 	 	 	 	 	 	 v a l u e   =   S y s t e m . M a t h . M a x ( v a l u e ,   0 ) ;  
 	 	 	 	 	 	 t h i s . o b j e c t M o d i f i e r . S e t W i d t h ( t h i s . o b j ,   v a l u e ) ;  
 	 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 	 c a s e   T y p e . H e i g h t :  
 	 	 	 	 	 	 v a l u e   =   S y s t e m . M a t h . M a x ( v a l u e ,   0 ) ;  
 	 	 	 	 	 	 t h i s . o b j e c t M o d i f i e r . S e t H e i g h t ( t h i s . o b j ,   v a l u e ) ;  
 	 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 	 c a s e   T y p e . M a r g i n L e f t :  
 	 	 	 	 	 	 v a l u e   =   S y s t e m . M a t h . M a x ( v a l u e ,   0 ) ;  
 	 	 	 	 	 	 m   =   t h i s . o b j e c t M o d i f i e r . G e t M a r g i n s ( t h i s . o b j ) ;  
 	 	 	 	 	 	 m . L e f t   =   v a l u e ;  
 	 	 	 	 	 	 t h i s . o b j e c t M o d i f i e r . S e t M a r g i n s ( t h i s . o b j ,   m ) ;  
 	 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 	 c a s e   T y p e . M a r g i n R i g h t :  
 	 	 	 	 	 	 v a l u e   =   S y s t e m . M a t h . M a x ( v a l u e ,   0 ) ;  
 	 	 	 	 	 	 m   =   t h i s . o b j e c t M o d i f i e r . G e t M a r g i n s ( t h i s . o b j ) ;  
 	 	 	 	 	 	 m . R i g h t   =   v a l u e ;  
 	 	 	 	 	 	 t h i s . o b j e c t M o d i f i e r . S e t M a r g i n s ( t h i s . o b j ,   m ) ;  
 	 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 	 c a s e   T y p e . M a r g i n B o t t o m :  
 	 	 	 	 	 	 v a l u e   =   S y s t e m . M a t h . M a x ( v a l u e ,   0 ) ;  
 	 	 	 	 	 	 m   =   t h i s . o b j e c t M o d i f i e r . G e t M a r g i n s ( t h i s . o b j ) ;  
 	 	 	 	 	 	 m . B o t t o m   =   v a l u e ;  
 	 	 	 	 	 	 t h i s . o b j e c t M o d i f i e r . S e t M a r g i n s ( t h i s . o b j ,   m ) ;  
 	 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 	 c a s e   T y p e . M a r g i n T o p :  
 	 	 	 	 	 	 v a l u e   =   S y s t e m . M a t h . M a x ( v a l u e ,   0 ) ;  
 	 	 	 	 	 	 m   =   t h i s . o b j e c t M o d i f i e r . G e t M a r g i n s ( t h i s . o b j ) ;  
 	 	 	 	 	 	 m . T o p   =   v a l u e ;  
 	 	 	 	 	 	 t h i s . o b j e c t M o d i f i e r . S e t M a r g i n s ( t h i s . o b j ,   m ) ;  
 	 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 	 c a s e   T y p e . P a d d i n g L e f t :  
 	 	 	 	 	 	 v a l u e   =   S y s t e m . M a t h . M a x ( v a l u e ,   0 ) ;  
 	 	 	 	 	 	 m   =   t h i s . o b j e c t M o d i f i e r . G e t P a d d i n g ( t h i s . o b j ) ;  
 	 	 	 	 	 	 m . L e f t   =   v a l u e ;  
 	 	 	 	 	 	 t h i s . o b j e c t M o d i f i e r . S e t P a d d i n g ( t h i s . o b j ,   m ) ;  
 	 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 	 c a s e   T y p e . P a d d i n g R i g h t :  
 	 	 	 	 	 	 v a l u e   =   S y s t e m . M a t h . M a x ( v a l u e ,   0 ) ;  
 	 	 	 	 	 	 m   =   t h i s . o b j e c t M o d i f i e r . G e t P a d d i n g ( t h i s . o b j ) ;  
 	 	 	 	 	 	 m . R i g h t   =   v a l u e ;  
 	 	 	 	 	 	 t h i s . o b j e c t M o d i f i e r . S e t P a d d i n g ( t h i s . o b j ,   m ) ;  
 	 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 	 c a s e   T y p e . P a d d i n g B o t t o m :  
 	 	 	 	 	 	 v a l u e   =   S y s t e m . M a t h . M a x ( v a l u e ,   0 ) ;  
 	 	 	 	 	 	 m   =   t h i s . o b j e c t M o d i f i e r . G e t P a d d i n g ( t h i s . o b j ) ;  
 	 	 	 	 	 	 m . B o t t o m   =   v a l u e ;  
 	 	 	 	 	 	 t h i s . o b j e c t M o d i f i e r . S e t P a d d i n g ( t h i s . o b j ,   m ) ;  
 	 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 	 c a s e   T y p e . P a d d i n g T o p :  
 	 	 	 	 	 	 v a l u e   =   S y s t e m . M a t h . M a x ( v a l u e ,   0 ) ;  
 	 	 	 	 	 	 m   =   t h i s . o b j e c t M o d i f i e r . G e t P a d d i n g ( t h i s . o b j ) ;  
 	 	 	 	 	 	 m . T o p   =   v a l u e ;  
 	 	 	 	 	 	 t h i s . o b j e c t M o d i f i e r . S e t P a d d i n g ( t h i s . o b j ,   m ) ;  
 	 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 	 c a s e   T y p e . G r i d W i d t h :  
 	 	 	 	 	 	 v a l u e   =   S y s t e m . M a t h . M a x ( v a l u e ,   0 ) ;  
 	 	 	 	 	 	 i f   ( t h i s . o b j e c t M o d i f i e r . G e t G r i d C o l u m n M o d e ( t h i s . o b j ,   t h i s . c o l u m n O r R o w )   = =   O b j e c t M o d i f i e r . G r i d M o d e . A u t o )  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 t h i s . o b j e c t M o d i f i e r . S e t G r i d C o l u m n M i n W i d t h ( t h i s . o b j ,   t h i s . c o l u m n O r R o w ,   v a l u e ) ;  
 	 	 	 	 	 	 }  
 	 	 	 	 	 	 e l s e  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 t h i s . o b j e c t M o d i f i e r . S e t G r i d C o l u m n W i d t h ( t h i s . o b j ,   t h i s . c o l u m n O r R o w ,   v a l u e ) ;  
 	 	 	 	 	 	 }  
 	 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 	 c a s e   T y p e . G r i d H e i g h t :  
 	 	 	 	 	 	 v a l u e   =   S y s t e m . M a t h . M a x ( v a l u e ,   0 ) ;  
 	 	 	 	 	 	 i f   ( t h i s . o b j e c t M o d i f i e r . G e t G r i d R o w M o d e ( t h i s . o b j ,   t h i s . c o l u m n O r R o w )   = =   O b j e c t M o d i f i e r . G r i d M o d e . A u t o )  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 t h i s . o b j e c t M o d i f i e r . S e t G r i d R o w M i n H e i g h t ( t h i s . o b j ,   t h i s . c o l u m n O r R o w ,   v a l u e ) ;  
 	 	 	 	 	 	 }  
 	 	 	 	 	 	 e l s e  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 t h i s . o b j e c t M o d i f i e r . S e t G r i d R o w H e i g h t ( t h i s . o b j ,   t h i s . c o l u m n O r R o w ,   v a l u e ) ;  
 	 	 	 	 	 	 }  
 	 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 	 c a s e   T y p e . G r i d M a r g i n L e f t :  
 	 	 	 	 	 	 v a l u e   =   S y s t e m . M a t h . M a x ( v a l u e ,   0 ) ;  
 	 	 	 	 	 	 t h i s . o b j e c t M o d i f i e r . S e t G r i d C o l u m n L e f t B o r d e r ( t h i s . o b j ,   t h i s . c o l u m n O r R o w ,   v a l u e ) ;  
 	 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 	 c a s e   T y p e . G r i d M a r g i n R i g h t :  
 	 	 	 	 	 	 v a l u e   =   S y s t e m . M a t h . M a x ( v a l u e ,   0 ) ;  
 	 	 	 	 	 	 t h i s . o b j e c t M o d i f i e r . S e t G r i d C o l u m n R i g h t B o r d e r ( t h i s . o b j ,   t h i s . c o l u m n O r R o w ,   v a l u e ) ;  
 	 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 	 c a s e   T y p e . G r i d M a r g i n B o t t o m :  
 	 	 	 	 	 	 v a l u e   =   S y s t e m . M a t h . M a x ( v a l u e ,   0 ) ;  
 	 	 	 	 	 	 t h i s . o b j e c t M o d i f i e r . S e t G r i d R o w B o t t o m B o r d e r ( t h i s . o b j ,   t h i s . c o l u m n O r R o w ,   v a l u e ) ;  
 	 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 	 c a s e   T y p e . G r i d M a r g i n T o p :  
 	 	 	 	 	 	 v a l u e   =   S y s t e m . M a t h . M a x ( v a l u e ,   0 ) ;  
 	 	 	 	 	 	 t h i s . o b j e c t M o d i f i e r . S e t G r i d R o w T o p B o r d e r ( t h i s . o b j ,   t h i s . c o l u m n O r R o w ,   v a l u e ) ;  
 	 	 	 	 	 	 b r e a k ;  
 	 	 	 	 }  
  
 	 	 	 	 t h i s . e d i t o r . I n v a l i d a t e ( ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p u b l i c   b o o l   D e t e c t ( P o i n t   m o u s e )  
 	 	 {  
 	 	 	 / / 	 D é t e c t e   s i   l a   s o u r i s   e s t   d a n s   l a   c o t e .  
 	 	 	 i f   ( t h i s . s l a v e )  
 	 	 	 {  
 	 	 	 	 r e t u r n   f a l s e ;  
 	 	 	 }  
  
 # i f   f a l s e  
 	 	 	 r e t u r n   t h i s . G e o m e t r y T e x t B o x . C o n t a i n s ( m o u s e ) ;  
 # e l s e  
 	 	 	 P a t h   p a t h   =   t h i s . G e o m e t r y B a c k g r o u n d ( n u l l ) ;  
 	 	 	 r e t u r n   I n s i d e S u r f a c e . C o n t a i n s ( p a t h ,   m o u s e ) ;  
 # e n d i f  
 	 	 }  
  
  
 	 	 p r o t e c t e d   v o i d   D r a w L i n e ( G r a p h i c s   g r a p h i c s ,   P o i n t   p 1 ,   P o i n t   p 2 )  
 	 	 {  
 	 	 	 / / 	 D e s s i n e   l e   t r a i t   d ' u n e   c o t e .  
 	 	 	 d o u b l e   d   =   P o i n t . D i s t a n c e ( p 1 ,   p 2 ) ;  
  
 	 	 	 i f   ( d   <   1 )  
 	 	 	 {  
 	 	 	 	 g r a p h i c s . A d d F i l l e d C i r c l e ( p 1 ,   2 ) ;  
 	 	 	 	 g r a p h i c s . R e n d e r S o l i d ( C o l o r . F r o m B r i g h t n e s s ( 0 ) ) ;  
 	 	 	 	 r e t u r n ;  
 	 	 	 }  
  
 	 	 	 d o u b l e   e   =   3 ;  
 	 	 	 d o u b l e   i   =   1 ;  
  
 	 	 	 i f   ( p 1 . Y   = =   p 2 . Y )     / /   h o r i z o n t a l   ?  
 	 	 	 {  
 	 	 	 	 S i z e   s e   =   n e w   S i z e ( 0 ,   e ) ;  
 	 	 	 	 S i z e   s i   =   n e w   S i z e ( 0 ,   i ) ;  
  
 	 	 	 	 p 1 . Y   + =   0 . 5 ;  
 	 	 	 	 p 2 . Y   + =   0 . 5 ;  
  
 	 	 	 	 g r a p h i c s . A d d L i n e ( p 1 ,   p 2 ) ;  
 	 	 	 	 i f   ( d   >   1 )  
 	 	 	 	 {  
 	 	 	 	 	 g r a p h i c s . A d d L i n e ( p 1 - s i ,   p 1 + s e ) ;  
 	 	 	 	 	 g r a p h i c s . A d d L i n e ( p 2 - s i ,   p 2 + s e ) ;  
 	 	 	 	 }  
 	 	 	 	 g r a p h i c s . R e n d e r S o l i d ( C o l o r . F r o m B r i g h t n e s s ( 0 ) ) ;  
 	 	 	 }  
  
 	 	 	 i f   ( p 1 . X   = =   p 2 . X )     / /   v e r t i c a l   ?  
 	 	 	 {  
 	 	 	 	 S i z e   s e   =   n e w   S i z e ( e ,   0 ) ;  
 	 	 	 	 S i z e   s i   =   n e w   S i z e ( i ,   0 ) ;  
  
 	 	 	 	 p 1 . X   + =   0 . 5 ;  
 	 	 	 	 p 2 . X   + =   0 . 5 ;  
  
 	 	 	 	 g r a p h i c s . A d d L i n e ( p 1 ,   p 2 ) ;  
 	 	 	 	 i f   ( d   >   1 )  
 	 	 	 	 {  
 	 	 	 	 	 g r a p h i c s . A d d L i n e ( p 1 - s i ,   p 1 + s e ) ;  
 	 	 	 	 	 g r a p h i c s . A d d L i n e ( p 2 - s i ,   p 2 + s e ) ;  
 	 	 	 	 }  
 	 	 	 	 g r a p h i c s . R e n d e r S o l i d ( C o l o r . F r o m B r i g h t n e s s ( 0 ) ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   D r a w S p r i n g ( G r a p h i c s   g r a p h i c s ,   P o i n t   p 1 ,   P o i n t   p 2 ,   C o l o r   c o l o r )  
 	 	 {  
 	 	 	 / / 	 D e s s i n e   u n   p e t i t   r e s s o r t   h o r i z o n t a l   o u   v e r t i c a l   d ' u n e   c o t e .  
 	 	 	 i f   ( P o i n t . D i s t a n c e ( p 1 ,   p 2 )   <   8 )  
 	 	 	 {  
 	 	 	 	 g r a p h i c s . A d d L i n e ( p 1 ,   p 2 ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 P o i n t   p 1 a   =   P o i n t . S c a l e ( p 1 ,   p 2 ,   D i m e n s i o n . a t t a c h m e n t S c a l e ) ;  
 	 	 	 	 P o i n t   p 2 a   =   P o i n t . S c a l e ( p 2 ,   p 1 ,   D i m e n s i o n . a t t a c h m e n t S c a l e ) ;  
  
 	 	 	 	 g r a p h i c s . A d d L i n e ( p 1 ,   p 1 a ) ;  
 	 	 	 	 g r a p h i c s . A d d L i n e ( p 2 ,   p 2 a ) ;  
  
 	 	 	 	 d o u b l e   d i m   =   D i m e n s i o n . a t t a c h m e n t T h i c k n e s s ;  
 	 	 	 	 d o u b l e   l e n g t h   =   P o i n t . D i s t a n c e ( p 1 a ,   p 2 a ) ;  
 	 	 	 	 i n t   l o o p s   =   ( i n t )   ( l e n g t h / ( d i m * 2 ) ) ;  
 	 	 	 	 l o o p s   =   S y s t e m . M a t h . M a x ( l o o p s ,   1 ) ;  
 	 	 	 	 M i s c . A d d S p r i n g ( g r a p h i c s ,   p 1 a ,   p 2 a ,   d i m ,   l o o p s ) ;  
 	 	 	 }  
  
 	 	 	 g r a p h i c s . R e n d e r S o l i d ( c o l o r ) ;  
  
 	 	 	 g r a p h i c s . A d d F i l l e d C i r c l e ( p 1 ,   1 . 5 ) ;  
 	 	 	 g r a p h i c s . A d d F i l l e d C i r c l e ( p 2 ,   1 . 5 ) ;  
 	 	 	 g r a p h i c s . R e n d e r S o l i d ( c o l o r ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   D r a w T e x t ( G r a p h i c s   g r a p h i c s ,   R e c t a n g l e   b o x )  
 	 	 {  
 	 	 	 / / 	 D e s s i n e   l a   v a l e u r   d ' u n e   c o t e   a v e c   d e s   p e t i t s   c a r a c t è r e s .  
 	 	 	 i f   ( t h i s . t y p e   = =   T y p e . H e i g h t               | |  
 	 	 	 	 t h i s . t y p e   = =   T y p e . M a r g i n B o t t o m   | |  
 	 	 	 	 t h i s . t y p e   = =   T y p e . M a r g i n T o p         | |  
 	 	 	 	 t h i s . t y p e   = =   T y p e . P a d d i n g R i g h t   )     / /   t e x t e   v e r t i c a l   ?  
 	 	 	 {  
 	 	 	 	 P o i n t   c e n t e r   =   b o x . C e n t e r ;  
 	 	 	 	 T r a n s f o r m   i t   =   g r a p h i c s . T r a n s f o r m ;  
 	 	 	 	 g r a p h i c s . R o t a t e T r a n s f o r m D e g ( - 9 0 ,   c e n t e r . X ,   c e n t e r . Y ) ;  
 	 	 	 	 g r a p h i c s . A d d T e x t ( b o x . L e f t ,   b o x . B o t t o m ,   b o x . W i d t h ,   b o x . H e i g h t ,   t h i s . S t r i n g V a l u e ,   F o n t . D e f a u l t F o n t ,   9 . 0 ,   C o n t e n t A l i g n m e n t . M i d d l e C e n t e r ) ;  
 	 	 	 	 g r a p h i c s . T r a n s f o r m   =   i t ;  
 	 	 	 }  
 	 	 	 e l s e   i f   ( t h i s . t y p e   = =   T y p e . P a d d i n g L e f t             | |  
 	 	 	 	 	   t h i s . t y p e   = =   T y p e . G r i d R o w                     | |  
 	 	 	 	 	   t h i s . t y p e   = =   T y p e . G r i d H e i g h t               | |  
 	 	 	 	 	   t h i s . t y p e   = =   T y p e . G r i d M a r g i n B o t t o m   | |  
 	 	 	 	 	   t h i s . t y p e   = =   T y p e . G r i d M a r g i n T o p         )  
 	 	 	 {  
 	 	 	 	 P o i n t   c e n t e r   =   b o x . C e n t e r ;  
 	 	 	 	 T r a n s f o r m   i t   =   g r a p h i c s . T r a n s f o r m ;  
 	 	 	 	 g r a p h i c s . R o t a t e T r a n s f o r m D e g ( 9 0 ,   c e n t e r . X ,   c e n t e r . Y ) ;  
 	 	 	 	 g r a p h i c s . A d d T e x t ( b o x . L e f t ,   b o x . B o t t o m ,   b o x . W i d t h ,   b o x . H e i g h t ,   t h i s . S t r i n g V a l u e ,   F o n t . D e f a u l t F o n t ,   9 . 0 ,   C o n t e n t A l i g n m e n t . M i d d l e C e n t e r ) ;  
 	 	 	 	 g r a p h i c s . T r a n s f o r m   =   i t ;  
 	 	 	 }  
 	 	 	 e l s e     / /   t e x t e   h o r i z o n t a l   ?  
 	 	 	 {  
 	 	 	 	 g r a p h i c s . A d d T e x t ( b o x . L e f t ,   b o x . B o t t o m ,   b o x . W i d t h ,   b o x . H e i g h t ,   t h i s . S t r i n g V a l u e ,   F o n t . D e f a u l t F o n t ,   9 . 0 ,   C o n t e n t A l i g n m e n t . M i d d l e C e n t e r ) ;  
 	 	 	 }  
  
 	 	 	 g r a p h i c s . R e n d e r S o l i d ( C o l o r . F r o m R g b ( 0 ,   0 ,   0 ) ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   s t r i n g   S t r i n g V a l u e  
 	 	 {  
 	 	 	 / / 	 R e t o u r n e   l a   c h a î n e   à   a f f i c h e r   c o m m e   v a l e u r   d e   l a   c o t e .  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 i f   ( t h i s . I s P e r c e n t )  
 	 	 	 	 {  
 	 	 	 	 	 r e t u r n   s t r i n g . C o n c a t ( t h i s . V a l u e . T o S t r i n g ( S y s t e m . G l o b a l i z a t i o n . C u l t u r e I n f o . I n v a r i a n t C u l t u r e ) ,   " % " ) ;  
 	 	 	 	 }  
  
 	 	 	 	 s w i t c h   ( t h i s . t y p e )  
 	 	 	 	 {  
 	 	 	 	 	 c a s e   T y p e . G r i d C o l u m n :  
 	 	 	 	 	 	 r e t u r n   D i m e n s i o n . T o A l p h a ( t h i s . c o l u m n O r R o w ) ;     / /   A . . Z Z Z  
  
 	 	 	 	 	 c a s e   T y p e . G r i d R o w :  
 	 	 	 	 	 	 r e t u r n   ( t h i s . c o l u m n O r R o w + 1 ) . T o S t r i n g ( S y s t e m . G l o b a l i z a t i o n . C u l t u r e I n f o . I n v a r i a n t C u l t u r e ) ;     / /   1 . . n  
  
 	 	 	 	 	 c a s e   T y p e . G r i d W i d t h M o d e :  
 	 	 	 	 	 	 r e t u r n   D i m e n s i o n . T o A l p h a ( t h i s . o b j e c t M o d i f i e r . G e t G r i d C o l u m n M o d e ( t h i s . o b j ,   t h i s . c o l u m n O r R o w ) ) ;  
  
 	 	 	 	 	 c a s e   T y p e . G r i d H e i g h t M o d e :  
 	 	 	 	 	 	 r e t u r n   D i m e n s i o n . T o A l p h a ( t h i s . o b j e c t M o d i f i e r . G e t G r i d R o w M o d e ( t h i s . o b j ,   t h i s . c o l u m n O r R o w ) ) ;  
  
 	 	 	 	 	 c a s e   T y p e . G r i d C o l u m n A d d B e f o r e :  
 	 	 	 	 	 c a s e   T y p e . G r i d C o l u m n A d d A f t e r :  
 	 	 	 	 	 c a s e   T y p e . G r i d R o w A d d B e f o r e :  
 	 	 	 	 	 c a s e   T y p e . G r i d R o w A d d A f t e r :  
 	 	 	 	 	 c a s e   T y p e . G r i d C o l u m n S p a n I n c :  
 	 	 	 	 	 c a s e   T y p e . G r i d R o w S p a n I n c :  
 	 	 	 	 	 	 r e t u r n   " + " ;  
  
 	 	 	 	 	 c a s e   T y p e . G r i d C o l u m n R e m o v e :  
 	 	 	 	 	 c a s e   T y p e . G r i d R o w R e m o v e :  
 	 	 	 	 	 c a s e   T y p e . G r i d C o l u m n S p a n D e c :  
 	 	 	 	 	 c a s e   T y p e . G r i d R o w S p a n D e c :  
 	 	 	 	 	 	 r e t u r n   " "" ;  
  
 	 	 	 	 	 c a s e   T y p e . C h i l d r e n P l a c e m e n t :  
 	 	 	 	 	 	 s w i t c h   ( t h i s . o b j e c t M o d i f i e r . G e t C h i l d r e n P l a c e m e n t ( t h i s . o b j ) )  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 c a s e   O b j e c t M o d i f i e r . C h i l d r e n P l a c e m e n t . H o r i z o n t a l S t a c k e d :     r e t u r n   " H " ;  
 	 	 	 	 	 	 	 c a s e   O b j e c t M o d i f i e r . C h i l d r e n P l a c e m e n t . V e r t i c a l S t a c k e d :         r e t u r n   " V " ;  
 	 	 	 	 	 	 	 c a s e   O b j e c t M o d i f i e r . C h i l d r e n P l a c e m e n t . G r i d :                               r e t u r n   " G " ;  
 	 	 	 	 	 	 	 d e f a u l t :                                                                                                     r e t u r n   " X " ;  
 	 	 	 	 	 	 }  
  
 	 	 	 	 	 d e f a u l t :  
 	 	 	 	 	 	 i n t   i   =   ( i n t )   S y s t e m . M a t h . F l o o r ( t h i s . V a l u e + 0 . 5 ) ;  
 	 	 	 	 	 	 r e t u r n   i . T o S t r i n g ( ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   s t a t i c   s t r i n g   T o A l p h a ( i n t   n )  
 	 	 {  
 	 	 	 / / 	 R e t o u r n e   u n   n o m b r e   e n   u n e   c h a î n e   e n   b a s e   2 6   ( d o n c   A . . Z ,   A A . . A Z ,   B A . . B Z ,   e t c . ) .  
 	 	 	 s t r i n g   t e x t   =   " " ;  
  
 	 	 	 d o  
 	 	 	 {  
 	 	 	 	 i n t   d i g i t   =   n % 2 6 ;  
 	 	 	 	 c h a r   c   =   ( c h a r )   ( ' A ' + d i g i t ) ;  
 	 	 	 	 t e x t   =   t e x t . I n s e r t ( 0 ,   c . T o S t r i n g ( ) ) ;  
  
 	 	 	 	 n   / =   2 6 ;  
 	 	 	 }  
 	 	 	 w h i l e   ( n   ! =   0 ) ;  
  
 	 	 	 r e t u r n   t e x t ;  
 	 	 }  
  
 	 	 p r o t e c t e d   s t a t i c   s t r i n g   T o A l p h a ( O b j e c t M o d i f i e r . G r i d M o d e   m o d e )  
 	 	 {  
 	 	 	 / / 	 R e t o u r n e   l e   m i n i - t e x t e   c o r r e s p o n d a n t   a u   m o d e   d e   l i g n e / c o l o n n e   d ' u n   t a b l e a u .  
 	 	 	 / / 	 C e   t e x t e   e s t   a f f i c h é   d a n s   u n   m i n u s c u l e   c a r r é   d e   1 2 x 1 2   p i x e l s .  
 	 	 	 s w i t c h   ( m o d e )  
 	 	 	 {  
 	 	 	 	 c a s e   O b j e c t M o d i f i e r . G r i d M o d e . A u t o :                     r e t u r n   " Ï%" ;  
 	 	 	 	 c a s e   O b j e c t M o d i f i e r . G r i d M o d e . A b s o l u t e :             r e t u r n   " # " ;  
 	 	 	 	 c a s e   O b j e c t M o d i f i e r . G r i d M o d e . P r o p o r t i o n a l :     r e t u r n   " % " ;  
 	 	 	 	 d e f a u l t :                                                                         r e t u r n   " " ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   b o o l   I s S i m p l e R e c t a n g l e  
 	 	 {  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   ( t h i s . t y p e   = =   T y p e . G r i d C o l u m n                   | |  
 	 	 	 	 	 	 t h i s . t y p e   = =   T y p e . G r i d R o w                         | |  
 	 	 	 	 	 	 t h i s . t y p e   = =   T y p e . G r i d W i d t h M o d e             | |  
 	 	 	 	 	 	 t h i s . t y p e   = =   T y p e . G r i d H e i g h t M o d e           | |  
 	 	 	 	 	 	 t h i s . t y p e   = =   T y p e . G r i d C o l u m n A d d A f t e r   | |  
 	 	 	 	 	 	 t h i s . t y p e   = =   T y p e . G r i d C o l u m n A d d B e f o r e | |  
 	 	 	 	 	 	 t h i s . t y p e   = =   T y p e . G r i d C o l u m n R e m o v e       | |  
 	 	 	 	 	 	 t h i s . t y p e   = =   T y p e . G r i d R o w A d d A f t e r         | |  
 	 	 	 	 	 	 t h i s . t y p e   = =   T y p e . G r i d R o w A d d B e f o r e       | |  
 	 	 	 	 	 	 t h i s . t y p e   = =   T y p e . G r i d R o w R e m o v e             | |  
 	 	 	 	 	 	 t h i s . t y p e   = =   T y p e . G r i d C o l u m n S p a n I n c     | |  
 	 	 	 	 	 	 t h i s . t y p e   = =   T y p e . G r i d C o l u m n S p a n D e c     | |  
 	 	 	 	 	 	 t h i s . t y p e   = =   T y p e . G r i d R o w S p a n I n c           | |  
 	 	 	 	 	 	 t h i s . t y p e   = =   T y p e . G r i d R o w S p a n D e c           | |  
 	 	 	 	 	 	 t h i s . t y p e   = =   T y p e . C h i l d r e n P l a c e m e n t   ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   b o o l   I s P e r c e n t  
 	 	 {  
 	 	 	 / / 	 I n d i q u e   s i   l a   c o t e   r e p r é s e n t e   u n e   v a l e u r   e n   p o u r c e n t s .  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 i f   ( t h i s . t y p e   = =   T y p e . G r i d W i d t h   & &  
 	 	 	 	 	 t h i s . o b j e c t M o d i f i e r . G e t G r i d C o l u m n M o d e ( t h i s . o b j ,   t h i s . c o l u m n O r R o w )   = =   O b j e c t M o d i f i e r . G r i d M o d e . P r o p o r t i o n a l )  
 	 	 	 	 {  
 	 	 	 	 	 r e t u r n   t r u e ;  
 	 	 	 	 }  
  
 	 	 	 	 i f   ( t h i s . t y p e   = =   T y p e . G r i d H e i g h t   & &  
 	 	 	 	 	 t h i s . o b j e c t M o d i f i e r . G e t G r i d R o w M o d e ( t h i s . o b j ,   t h i s . c o l u m n O r R o w )   = =   O b j e c t M o d i f i e r . G r i d M o d e . P r o p o r t i o n a l )  
 	 	 	 	 {  
 	 	 	 	 	 r e t u r n   t r u e ;  
 	 	 	 	 }  
  
 	 	 	 	 r e t u r n   f a l s e ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   b o o l   I s G r i d S e l e c t e d  
 	 	 {  
 	 	 	 / / 	 I n d i q u e   s i   l a   l i g n e / c o l o n n e   c o r r e s p o n d a n t e   d a n s   l e   t a b l e a u   e s t   s é l e c t i o n n é e .  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 i f   ( t h i s . t y p e   = =   T y p e . G r i d C o l u m n   | |   t h i s . t y p e   = =   T y p e . G r i d R o w )  
 	 	 	 	 {  
 	 	 	 	 	 G r i d S e l e c t i o n   g s   =   G r i d S e l e c t i o n . G e t ( t h i s . o b j ) ;  
 	 	 	 	 	 i f   ( g s   ! =   n u l l )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 G r i d S e l e c t i o n . U n i t   u n i t   =   ( t h i s . t y p e   = =   T y p e . G r i d C o l u m n )   ?   G r i d S e l e c t i o n . U n i t . C o l u m n   :   G r i d S e l e c t i o n . U n i t . R o w ;  
 	 	 	 	 	 	 r e t u r n   g s . S e a r c h ( u n i t ,   t h i s . c o l u m n O r R o w )   ! =   - 1 ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
  
 	 	 	 	 r e t u r n   f a l s e ;  
 	 	 	 }  
 	 	 }  
  
  
 	 	 p r o t e c t e d   C o l o r   B a c k g r o u n d C o l o r  
 	 	 {  
 	 	 	 / / 	 R e t o u r n e   l a   c o u l e u r   d e   f o n d .  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 s w i t c h   ( t h i s . t y p e )  
 	 	 	 	 {  
 	 	 	 	 	 c a s e   T y p e . W i d t h :  
 	 	 	 	 	 c a s e   T y p e . H e i g h t :  
 	 	 	 	 	 c a s e   T y p e . G r i d W i d t h :  
 	 	 	 	 	 c a s e   T y p e . G r i d H e i g h t :  
 	 	 	 	 	 c a s e   T y p e . G r i d W i d t h M o d e :  
 	 	 	 	 	 c a s e   T y p e . G r i d H e i g h t M o d e :  
 	 	 	 	 	 c a s e   T y p e . C h i l d r e n P l a c e m e n t :  
 	 	 	 	 	 	 r e t u r n   C o l o r . F r o m A l p h a R g b ( 0 . 8 ,   2 5 5 . 0 / 2 5 5 . 0 ,   1 8 0 . 0 / 2 5 5 . 0 ,   1 3 0 . 0 / 2 5 5 . 0 ) ;     / /   r o u g e  
  
 	 	 	 	 	 c a s e   T y p e . M a r g i n L e f t :  
 	 	 	 	 	 c a s e   T y p e . M a r g i n R i g h t :  
 	 	 	 	 	 c a s e   T y p e . M a r g i n B o t t o m :  
 	 	 	 	 	 c a s e   T y p e . M a r g i n T o p :  
 	 	 	 	 	 c a s e   T y p e . G r i d M a r g i n L e f t :  
 	 	 	 	 	 c a s e   T y p e . G r i d M a r g i n R i g h t :  
 	 	 	 	 	 c a s e   T y p e . G r i d M a r g i n B o t t o m :  
 	 	 	 	 	 c a s e   T y p e . G r i d M a r g i n T o p :  
 	 	 	 	 	 	 r e t u r n   C o l o r . F r o m A l p h a R g b ( 0 . 8 ,   2 5 5 . 0 / 2 5 5 . 0 ,   2 2 0 . 0 / 2 5 5 . 0 ,   1 3 0 . 0 / 2 5 5 . 0 ) ;     / /   o r a n g e  
  
 	 	 	 	 	 c a s e   T y p e . P a d d i n g L e f t :  
 	 	 	 	 	 c a s e   T y p e . P a d d i n g R i g h t :  
 	 	 	 	 	 c a s e   T y p e . P a d d i n g B o t t o m :  
 	 	 	 	 	 c a s e   T y p e . P a d d i n g T o p :  
 	 	 	 	 	 	 r e t u r n   C o l o r . F r o m A l p h a R g b ( 0 . 8 ,   2 5 5 . 0 / 2 5 5 . 0 ,   2 5 5 . 0 / 2 5 5 . 0 ,   1 3 0 . 0 / 2 5 5 . 0 ) ;     / /   j a u n e  
  
 	 	 	 	 	 c a s e   T y p e . G r i d C o l u m n :  
 	 	 	 	 	 c a s e   T y p e . G r i d R o w :  
 	 	 	 	 	 	 i f   ( t h i s . I s G r i d S e l e c t e d )  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 r e t u r n   C o l o r . F r o m R g b ( 2 5 5 . 0 / 2 5 5 . 0 ,   2 5 5 . 0 / 2 5 5 . 0 ,   2 0 0 . 0 / 2 5 5 . 0 ) ;  
 	 	 	 	 	 	 }  
 	 	 	 	 	 	 e l s e  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 r e t u r n   M i s c . A l p h a C o l o r ( P a n e l s C o n t e x t . C o l o r H i l i t e S u r f a c e ,   0 . 8 )   ;  
 	 	 	 	 	 	 }  
  
 	 	 	 	 	 c a s e   T y p e . G r i d C o l u m n A d d B e f o r e :  
 	 	 	 	 	 c a s e   T y p e . G r i d C o l u m n A d d A f t e r :  
 	 	 	 	 	 c a s e   T y p e . G r i d C o l u m n R e m o v e :  
 	 	 	 	 	 c a s e   T y p e . G r i d R o w A d d B e f o r e :  
 	 	 	 	 	 c a s e   T y p e . G r i d R o w A d d A f t e r :  
 	 	 	 	 	 c a s e   T y p e . G r i d R o w R e m o v e :  
 	 	 	 	 	 c a s e   T y p e . G r i d C o l u m n S p a n I n c :  
 	 	 	 	 	 c a s e   T y p e . G r i d C o l u m n S p a n D e c :  
 	 	 	 	 	 c a s e   T y p e . G r i d R o w S p a n I n c :  
 	 	 	 	 	 c a s e   T y p e . G r i d R o w S p a n D e c :  
 	 	 	 	 	 	 r e t u r n   C o l o r . F r o m R g b ( 2 5 5 . 0 / 2 5 5 . 0 ,   2 5 5 . 0 / 2 5 5 . 0 ,   2 0 0 . 0 / 2 5 5 . 0 ) ;  
  
 	 	 	 	 	 d e f a u l t :  
 	 	 	 	 	 	 r e t u r n   C o l o r . F r o m B r i g h t n e s s ( 1 ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   P a t h   G e o m e t r y H i l i t e d S u r f a c e ( G r a p h i c s   g r a p h i c s )  
 	 	 {  
 	 	 	 / / 	 R e t o u r n e   l e   c h e m i n   d e   l a   s u r f a c e   p o u r   l a   m i s e   e n   é v i d e n c e .  
 	 	 	 R e c t a n g l e   b o x   =   t h i s . G e o m e t r y T e x t B o x ;  
 	 	 	 M i s c . A l i g n F o r L i n e ( g r a p h i c s ,   r e f   b o x ) ;  
 	 	 	 d o u b l e   t   =   2 0 ;  
  
 	 	 	 P a t h   p a t h   =   n e w   P a t h ( ) ;  
  
 	 	 	 i f   ( t h i s . I s S i m p l e R e c t a n g l e )  
 	 	 	 {  
 	 	 	 	 p a t h . A p p e n d R e c t a n g l e ( b o x ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 p a t h . M o v e T o ( b o x . T o p L e f t ) ;  
 	 	 	 	 p a t h . L i n e T o ( b o x . C e n t e r . X - t / 2 ,   b o x . T o p ) ;  
 	 	 	 	 p a t h . L i n e T o ( b o x . C e n t e r . X ,   b o x . T o p + t / 2 ) ;     / /   b o s s e   ' ^ '   s u p é r i e u r e ,   p o u r   ' + '  
 	 	 	 	 p a t h . L i n e T o ( b o x . C e n t e r . X + t / 2 ,   b o x . T o p ) ;  
 	 	 	 	 p a t h . L i n e T o ( b o x . T o p R i g h t ) ;  
 	 	 	 	 p a t h . L i n e T o ( b o x . B o t t o m R i g h t ) ;  
 	 	 	 	 p a t h . L i n e T o ( b o x . C e n t e r . X + t / 2 ,   b o x . B o t t o m ) ;  
 	 	 	 	 p a t h . L i n e T o ( b o x . C e n t e r . X ,   b o x . B o t t o m - t / 2 ) ;     / /   b o s s e   ' v '   i n f é r i e u r e ,   p o u r   ' - '  
 	 	 	 	 p a t h . L i n e T o ( b o x . C e n t e r . X - t / 2 ,   b o x . B o t t o m ) ;  
 	 	 	 	 p a t h . L i n e T o ( b o x . B o t t o m L e f t ) ;  
 	 	 	 	 p a t h . C l o s e ( ) ;  
 	 	 	 }  
  
 	 	 	 r e t u r n   p a t h ;  
 	 	 }  
  
 	 	 p r o t e c t e d   P a t h   G e o m e t r y B a c k g r o u n d ( G r a p h i c s   g r a p h i c s )  
 	 	 {  
 	 	 	 / / 	 R e t o u r n e   l e   c h e m i n   d e   l a   s u r f a c e   d e   f o n d .  
 	 	 	 R e c t a n g l e   b o u n d s   =   t h i s . o b j e c t M o d i f i e r . G e t A c t u a l B o u n d s ( t h i s . o b j ) ;  
 	 	 	 M a r g i n s   m a r g i n s   =   t h i s . o b j e c t M o d i f i e r . G e t M a r g i n s ( t h i s . o b j ) ;  
 	 	 	 R e c t a n g l e   e x t   =   b o u n d s ;  
 	 	 	 e x t . I n f l a t e ( t h i s . o b j e c t M o d i f i e r . G e t M a r g i n s ( t h i s . o b j ) ) ;  
 	 	 	 M a r g i n s   p a d d i n g   =   t h i s . o b j e c t M o d i f i e r . G e t P a d d i n g ( t h i s . o b j ) ;  
 	 	 	 R e c t a n g l e   i n s i d e   =   t h i s . o b j e c t M o d i f i e r . G e t F i n a l P a d d i n g ( t h i s . o b j ) ;  
  
 	 	 	 R e c t a n g l e   b o x   =   t h i s . G e o m e t r y T e x t B o x ;  
 	 	 	 M i s c . A l i g n F o r L i n e ( g r a p h i c s ,   r e f   b o x ) ;  
 	 	 	 M i s c . A l i g n F o r L i n e ( g r a p h i c s ,   r e f   b o u n d s ) ;  
 	 	 	 M i s c . A l i g n F o r L i n e ( g r a p h i c s ,   r e f   e x t ) ;  
 	 	 	 M i s c . A l i g n F o r L i n e ( g r a p h i c s ,   r e f   i n s i d e ) ;  
  
 	 	 	 P a t h   p a t h   =   n u l l ;  
 	 	 	 R e c t a n g l e   r ;  
 	 	 	 P o i n t   p ;  
  
 	 	 	 s w i t c h   ( t h i s . t y p e )  
 	 	 	 {  
 	 	 	 	 c a s e   T y p e . W i d t h :  
 	 	 	 	 	 r   =   b o x ;  
 	 	 	 	 	 r . T o p   =   e x t . B o t t o m ;  
 	 	 	 	 	 p a t h   =   n e w   P a t h ( ) ;  
 	 	 	 	 	 p a t h . A p p e n d R e c t a n g l e ( r ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . H e i g h t :  
 	 	 	 	 	 r   =   b o x ;  
 	 	 	 	 	 r . L e f t   =   e x t . R i g h t ;  
 	 	 	 	 	 p a t h   =   n e w   P a t h ( ) ;  
 	 	 	 	 	 p a t h . A p p e n d R e c t a n g l e ( r ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . M a r g i n L e f t :  
 	 	 	 	 	 p   =   n e w   P o i n t ( b o x . R i g h t ,   e x t . B o t t o m ) ;  
 	 	 	 	 	 p a t h   =   n e w   P a t h ( ) ;  
 	 	 	 	 	 p a t h . M o v e T o ( p ) ;  
 	 	 	 	 	 p . Y   =   b o x . B o t t o m ;  
 	 	 	 	 	 p a t h . L i n e T o ( p ) ;  
 	 	 	 	 	 p . X   - =   b o x . W i d t h ;  
 	 	 	 	 	 p a t h . L i n e T o ( p ) ;  
 	 	 	 	 	 p . Y   + =   b o x . H e i g h t ;  
 	 	 	 	 	 p a t h . L i n e T o ( p ) ;  
 	 	 	 	 	 p . Y   =   e x t . B o t t o m ;  
 	 	 	 	 	 p . X   =   b o x . R i g h t - m a r g i n s . L e f t ;  
 	 	 	 	 	 p a t h . L i n e T o ( p ) ;  
 	 	 	 	 	 p a t h . C l o s e ( ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . M a r g i n R i g h t :  
 	 	 	 	 	 p   =   n e w   P o i n t ( b o x . L e f t ,   e x t . B o t t o m ) ;  
 	 	 	 	 	 p a t h   =   n e w   P a t h ( ) ;  
 	 	 	 	 	 p a t h . M o v e T o ( p ) ;  
 	 	 	 	 	 p . Y   =   b o x . B o t t o m ;  
 	 	 	 	 	 p a t h . L i n e T o ( p ) ;  
 	 	 	 	 	 p . X   + =   b o x . W i d t h ;  
 	 	 	 	 	 p a t h . L i n e T o ( p ) ;  
 	 	 	 	 	 p . Y   + =   b o x . H e i g h t ;  
 	 	 	 	 	 p a t h . L i n e T o ( p ) ;  
 	 	 	 	 	 p . Y   =   e x t . B o t t o m ;  
 	 	 	 	 	 p . X   =   b o x . L e f t + m a r g i n s . R i g h t ;  
 	 	 	 	 	 p a t h . L i n e T o ( p ) ;  
 	 	 	 	 	 p a t h . C l o s e ( ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . M a r g i n B o t t o m :  
 	 	 	 	 	 p   =   n e w   P o i n t ( e x t . R i g h t ,   b o x . T o p ) ;  
 	 	 	 	 	 p a t h   =   n e w   P a t h ( ) ;  
 	 	 	 	 	 p a t h . M o v e T o ( p ) ;  
 	 	 	 	 	 p . X   =   b o x . R i g h t ;  
 	 	 	 	 	 p a t h . L i n e T o ( p ) ;  
 	 	 	 	 	 p . Y   - =   b o x . H e i g h t ;  
 	 	 	 	 	 p a t h . L i n e T o ( p ) ;  
 	 	 	 	 	 p . X   - =   b o x . W i d t h ;  
 	 	 	 	 	 p a t h . L i n e T o ( p ) ;  
 	 	 	 	 	 p . X   =   e x t . R i g h t ;  
 	 	 	 	 	 p . Y   =   b o x . T o p - m a r g i n s . B o t t o m ;  
 	 	 	 	 	 p a t h . L i n e T o ( p ) ;  
 	 	 	 	 	 p a t h . C l o s e ( ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . M a r g i n T o p :  
 	 	 	 	 	 p   =   n e w   P o i n t ( e x t . R i g h t ,   b o x . B o t t o m ) ;  
 	 	 	 	 	 p a t h   =   n e w   P a t h ( ) ;  
 	 	 	 	 	 p a t h . M o v e T o ( p ) ;  
 	 	 	 	 	 p . X   =   b o x . R i g h t ;  
 	 	 	 	 	 p a t h . L i n e T o ( p ) ;  
 	 	 	 	 	 p . Y   + =   b o x . H e i g h t ;  
 	 	 	 	 	 p a t h . L i n e T o ( p ) ;  
 	 	 	 	 	 p . X   - =   b o x . W i d t h ;  
 	 	 	 	 	 p a t h . L i n e T o ( p ) ;  
 	 	 	 	 	 p . X   =   e x t . R i g h t ;  
 	 	 	 	 	 p . Y   =   b o x . B o t t o m + m a r g i n s . T o p ;  
 	 	 	 	 	 p a t h . L i n e T o ( p ) ;  
 	 	 	 	 	 p a t h . C l o s e ( ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . P a d d i n g L e f t :  
 	 	 	 	 	 p a t h   =   n e w   P a t h ( ) ;  
 	 	 	 	 	 p a t h . M o v e T o ( b o u n d s . L e f t ,   i n s i d e . C e n t e r . Y ) ;  
 	 	 	 	 	 p a t h . L i n e T o ( b o x . B o t t o m L e f t ) ;  
 	 	 	 	 	 p a t h . L i n e T o ( b o x . B o t t o m R i g h t ) ;  
 	 	 	 	 	 p a t h . L i n e T o ( b o x . T o p R i g h t ) ;  
 	 	 	 	 	 p a t h . L i n e T o ( b o x . T o p L e f t ) ;  
 	 	 	 	 	 p a t h . C l o s e ( ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . P a d d i n g R i g h t :  
 	 	 	 	 	 p a t h   =   n e w   P a t h ( ) ;  
 	 	 	 	 	 p a t h . M o v e T o ( b o u n d s . R i g h t ,   i n s i d e . C e n t e r . Y ) ;  
 	 	 	 	 	 p a t h . L i n e T o ( b o x . B o t t o m R i g h t ) ;  
 	 	 	 	 	 p a t h . L i n e T o ( b o x . B o t t o m L e f t ) ;  
 	 	 	 	 	 p a t h . L i n e T o ( b o x . T o p L e f t ) ;  
 	 	 	 	 	 p a t h . L i n e T o ( b o x . T o p R i g h t ) ;  
 	 	 	 	 	 p a t h . C l o s e ( ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . P a d d i n g B o t t o m :  
 	 	 	 	 	 p a t h   =   n e w   P a t h ( ) ;  
 	 	 	 	 	 p a t h . M o v e T o ( i n s i d e . C e n t e r . X ,   b o u n d s . B o t t o m ) ;  
 	 	 	 	 	 p a t h . L i n e T o ( b o x . B o t t o m L e f t ) ;  
 	 	 	 	 	 p a t h . L i n e T o ( b o x . T o p L e f t ) ;  
 	 	 	 	 	 p a t h . L i n e T o ( b o x . T o p R i g h t ) ;  
 	 	 	 	 	 p a t h . L i n e T o ( b o x . B o t t o m R i g h t ) ;  
 	 	 	 	 	 p a t h . C l o s e ( ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . P a d d i n g T o p :  
 	 	 	 	 	 p a t h   =   n e w   P a t h ( ) ;  
 	 	 	 	 	 p a t h . M o v e T o ( i n s i d e . C e n t e r . X ,   b o u n d s . T o p ) ;  
 	 	 	 	 	 p a t h . L i n e T o ( b o x . T o p L e f t ) ;  
 	 	 	 	 	 p a t h . L i n e T o ( b o x . B o t t o m L e f t ) ;  
 	 	 	 	 	 p a t h . L i n e T o ( b o x . B o t t o m R i g h t ) ;  
 	 	 	 	 	 p a t h . L i n e T o ( b o x . T o p R i g h t ) ;  
 	 	 	 	 	 p a t h . C l o s e ( ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . G r i d C o l u m n :  
 	 	 	 	 	 r   =   b o x ;  
 	 	 	 	 	 r . B o t t o m   =   t h i s . I s G r i d S e l e c t e d   ?   i n s i d e . T o p   :   e x t . T o p ;  
 	 	 	 	 	 p a t h   =   n e w   P a t h ( ) ;  
 	 	 	 	 	 p a t h . A p p e n d R e c t a n g l e ( r ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . G r i d R o w :  
 	 	 	 	 	 r   =   b o x ;  
 	 	 	 	 	 r . R i g h t   =   t h i s . I s G r i d S e l e c t e d   ?   i n s i d e . L e f t   :   e x t . L e f t ;  
 	 	 	 	 	 p a t h   =   n e w   P a t h ( ) ;  
 	 	 	 	 	 p a t h . A p p e n d R e c t a n g l e ( r ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . G r i d W i d t h :  
 	 	 	 	 	 r   =   b o x ;  
 	 	 	 	 	 r . B o t t o m   =   e x t . T o p ;  
 	 	 	 	 	 p a t h   =   n e w   P a t h ( ) ;  
 	 	 	 	 	 p a t h . A p p e n d R e c t a n g l e ( r ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . G r i d H e i g h t :  
 	 	 	 	 	 r   =   b o x ;  
 	 	 	 	 	 r . R i g h t   =   e x t . L e f t ;  
 	 	 	 	 	 p a t h   =   n e w   P a t h ( ) ;  
 	 	 	 	 	 p a t h . A p p e n d R e c t a n g l e ( r ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . G r i d W i d t h M o d e :  
 	 	 	 	 	 p a t h   =   n e w   P a t h ( ) ;  
 	 	 	 	 	 p a t h . A p p e n d R e c t a n g l e ( b o x ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . G r i d H e i g h t M o d e :  
 	 	 	 	 	 p a t h   =   n e w   P a t h ( ) ;  
 	 	 	 	 	 p a t h . A p p e n d R e c t a n g l e ( b o x ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . G r i d M a r g i n L e f t :  
 	 	 	 	 	 p   =   n e w   P o i n t ( b o x . R i g h t ,   e x t . T o p ) ;  
 	 	 	 	 	 p a t h   =   n e w   P a t h ( ) ;  
 	 	 	 	 	 p a t h . M o v e T o ( p ) ;  
 	 	 	 	 	 p . Y   =   b o x . T o p ;  
 	 	 	 	 	 p a t h . L i n e T o ( p ) ;  
 	 	 	 	 	 p . X   - =   b o x . W i d t h ;  
 	 	 	 	 	 p a t h . L i n e T o ( p ) ;  
 	 	 	 	 	 p . Y   - =   b o x . H e i g h t ;  
 	 	 	 	 	 p a t h . L i n e T o ( p ) ;  
 	 	 	 	 	 p . Y   =   e x t . T o p ;  
 	 	 	 	 	 p . X   =   b o x . R i g h t - t h i s . V a l u e ;  
 	 	 	 	 	 p a t h . L i n e T o ( p ) ;  
 	 	 	 	 	 p a t h . C l o s e ( ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . G r i d M a r g i n R i g h t :  
 	 	 	 	 	 p   =   n e w   P o i n t ( b o x . L e f t ,   e x t . T o p ) ;  
 	 	 	 	 	 p a t h   =   n e w   P a t h ( ) ;  
 	 	 	 	 	 p a t h . M o v e T o ( p ) ;  
 	 	 	 	 	 p . Y   =   b o x . T o p ;  
 	 	 	 	 	 p a t h . L i n e T o ( p ) ;  
 	 	 	 	 	 p . X   + =   b o x . W i d t h ;  
 	 	 	 	 	 p a t h . L i n e T o ( p ) ;  
 	 	 	 	 	 p . Y   - =   b o x . H e i g h t ;  
 	 	 	 	 	 p a t h . L i n e T o ( p ) ;  
 	 	 	 	 	 p . Y   =   e x t . T o p ;  
 	 	 	 	 	 p . X   =   b o x . L e f t + t h i s . V a l u e ;  
 	 	 	 	 	 p a t h . L i n e T o ( p ) ;  
 	 	 	 	 	 p a t h . C l o s e ( ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . G r i d M a r g i n B o t t o m :  
 	 	 	 	 	 p   =   n e w   P o i n t ( e x t . L e f t ,   b o x . T o p ) ;  
 	 	 	 	 	 p a t h   =   n e w   P a t h ( ) ;  
 	 	 	 	 	 p a t h . M o v e T o ( p ) ;  
 	 	 	 	 	 p . X   =   b o x . L e f t ;  
 	 	 	 	 	 p a t h . L i n e T o ( p ) ;  
 	 	 	 	 	 p . Y   - =   b o x . H e i g h t ;  
 	 	 	 	 	 p a t h . L i n e T o ( p ) ;  
 	 	 	 	 	 p . X   + =   b o x . W i d t h ;  
 	 	 	 	 	 p a t h . L i n e T o ( p ) ;  
 	 	 	 	 	 p . X   =   e x t . L e f t ;  
 	 	 	 	 	 p . Y   =   b o x . T o p - t h i s . V a l u e ;  
 	 	 	 	 	 p a t h . L i n e T o ( p ) ;  
 	 	 	 	 	 p a t h . C l o s e ( ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . G r i d M a r g i n T o p :  
 	 	 	 	 	 p   =   n e w   P o i n t ( e x t . L e f t ,   b o x . B o t t o m ) ;  
 	 	 	 	 	 p a t h   =   n e w   P a t h ( ) ;  
 	 	 	 	 	 p a t h . M o v e T o ( p ) ;  
 	 	 	 	 	 p . X   =   b o x . L e f t ;  
 	 	 	 	 	 p a t h . L i n e T o ( p ) ;  
 	 	 	 	 	 p . Y   + =   b o x . H e i g h t ;  
 	 	 	 	 	 p a t h . L i n e T o ( p ) ;  
 	 	 	 	 	 p . X   + =   b o x . W i d t h ;  
 	 	 	 	 	 p a t h . L i n e T o ( p ) ;  
 	 	 	 	 	 p . X   =   e x t . L e f t ;  
 	 	 	 	 	 p . Y   =   b o x . B o t t o m + t h i s . V a l u e ;  
 	 	 	 	 	 p a t h . L i n e T o ( p ) ;  
 	 	 	 	 	 p a t h . C l o s e ( ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   T y p e . G r i d C o l u m n A d d B e f o r e :  
 	 	 	 	 c a s e   T y p e . G r i d C o l u m n A d d A f t e r :  
 	 	 	 	 c a s e   T y p e . G r i d C o l u m n R e m o v e :  
 	 	 	 	 c a s e   T y p e . G r i d R o w A d d B e f o r e :  
 	 	 	 	 c a s e   T y p e . G r i d R o w A d d A f t e r :  
 	 	 	 	 c a s e   T y p e . G r i d R o w R e m o v e :  
 	 	 	 	 c a s e   T y p e . G r i d C o l u m n S p a n I n c :  
 	 	 	 	 c a s e   T y p e . G r i d C o l u m n S p a n D e c :  
 	 	 	 	 c a s e   T y p e . G r i d R o w S p a n I n c :  
 	 	 	 	 c a s e   T y p e . G r i d R o w S p a n D e c :  
 	 	 	 	 c a s e   T y p e . C h i l d r e n P l a c e m e n t :  
 	 	 	 	 	 p a t h   =   n e w   P a t h ( ) ;  
 	 	 	 	 	 p a t h . A p p e n d R e c t a n g l e ( b o x ) ;  
 	 	 	 	 	 b r e a k ;  
 	 	 	 }  
  
 	 	 	 r e t u r n   p a t h ;  
 	 	 }  
  
 	 	 p r o t e c t e d   R e c t a n g l e   G e o m e t r y T e x t B o x  
 	 	 {  
 	 	 	 / / 	 R e t o u r n e   l e   r e c t a n g l e   p o u r   l e   t e x t e   d ' u n e   c o t e .  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 R e c t a n g l e   b o u n d s   =   t h i s . o b j e c t M o d i f i e r . G e t A c t u a l B o u n d s ( t h i s . o b j ) ;  
 	 	 	 	 M a r g i n s   m a r g i n s   =   t h i s . o b j e c t M o d i f i e r . G e t M a r g i n s ( t h i s . o b j ) ;  
 	 	 	 	 R e c t a n g l e   e x t   =   b o u n d s ;  
 	 	 	 	 e x t . I n f l a t e ( t h i s . o b j e c t M o d i f i e r . G e t M a r g i n s ( t h i s . o b j ) ) ;  
 	 	 	 	 R e c t a n g l e   i n s i d e   =   t h i s . o b j e c t M o d i f i e r . G e t F i n a l P a d d i n g ( t h i s . o b j ) ;  
  
 	 	 	 	 d o u b l e   d   =   2 6 ;  
 	 	 	 	 d o u b l e   h   =   1 2 ;  
 	 	 	 	 d o u b l e   e   =   1 0 ;  
 	 	 	 	 d o u b l e   p w   =   2 0 ;  
 	 	 	 	 d o u b l e   p h   =   1 2 ;  
 	 	 	 	 d o u b l e   l ;  
  
 	 	 	 	 R e c t a n g l e   b o x ;  
  
 	 	 	 	 s w i t c h   ( t h i s . t y p e )  
 	 	 	 	 {  
 	 	 	 	 	 c a s e   T y p e . W i d t h :  
 	 	 	 	 	 	 b o x   =   b o u n d s ;  
 	 	 	 	 	 	 b o x . B o t t o m   =   e x t . B o t t o m - d ;  
 	 	 	 	 	 	 b o x . H e i g h t   =   h ;  
 	 	 	 	 	 	 r e t u r n   b o x ;  
  
 	 	 	 	 	 c a s e   T y p e . H e i g h t :  
 	 	 	 	 	 	 b o x   =   b o u n d s ;  
 	 	 	 	 	 	 b o x . L e f t   =   e x t . R i g h t + d - h ;  
 	 	 	 	 	 	 b o x . W i d t h   =   h ;  
 	 	 	 	 	 	 r e t u r n   b o x ;  
  
 	 	 	 	 	 c a s e   T y p e . M a r g i n L e f t :  
 	 	 	 	 	 	 l   =   S y s t e m . M a t h . M a x ( e ,   m a r g i n s . L e f t ) ;  
 	 	 	 	 	 	 r e t u r n   n e w   R e c t a n g l e ( b o u n d s . L e f t - l ,   e x t . B o t t o m - d ,   l ,   h ) ;  
  
 	 	 	 	 	 c a s e   T y p e . M a r g i n R i g h t :  
 	 	 	 	 	 	 l   =   S y s t e m . M a t h . M a x ( e ,   m a r g i n s . R i g h t ) ;  
 	 	 	 	 	 	 r e t u r n   n e w   R e c t a n g l e ( b o u n d s . R i g h t ,   e x t . B o t t o m - d ,   l ,   h ) ;  
  
 	 	 	 	 	 c a s e   T y p e . M a r g i n T o p :  
 	 	 	 	 	 	 l   =   S y s t e m . M a t h . M a x ( e ,   m a r g i n s . T o p ) ;  
 	 	 	 	 	 	 r e t u r n   n e w   R e c t a n g l e ( e x t . R i g h t + d - h ,   b o u n d s . T o p ,   h ,   l ) ;  
  
 	 	 	 	 	 c a s e   T y p e . M a r g i n B o t t o m :  
 	 	 	 	 	 	 l   =   S y s t e m . M a t h . M a x ( e ,   m a r g i n s . B o t t o m ) ;  
 	 	 	 	 	 	 r e t u r n   n e w   R e c t a n g l e ( e x t . R i g h t + d - h ,   b o u n d s . B o t t o m - l ,   h ,   l ) ;  
  
 	 	 	 	 	 c a s e   T y p e . P a d d i n g L e f t :  
 	 	 	 	 	 	 b o x   =   t h i s . o b j e c t M o d i f i e r . G e t F i n a l P a d d i n g ( t h i s . o b j ) ;  
 	 	 	 	 	 	 r e t u r n   n e w   R e c t a n g l e ( i n s i d e . L e f t ,   i n s i d e . C e n t e r . Y - p w / 2 ,   p h ,   p w ) ;  
  
 	 	 	 	 	 c a s e   T y p e . P a d d i n g R i g h t :  
 	 	 	 	 	 	 b o x   =   t h i s . o b j e c t M o d i f i e r . G e t F i n a l P a d d i n g ( t h i s . o b j ) ;  
 	 	 	 	 	 	 r e t u r n   n e w   R e c t a n g l e ( i n s i d e . R i g h t - p h ,   i n s i d e . C e n t e r . Y - p w / 2 ,   p h ,   p w ) ;  
  
 	 	 	 	 	 c a s e   T y p e . P a d d i n g T o p :  
 	 	 	 	 	 	 b o x   =   t h i s . o b j e c t M o d i f i e r . G e t F i n a l P a d d i n g ( t h i s . o b j ) ;  
 	 	 	 	 	 	 r e t u r n   n e w   R e c t a n g l e ( i n s i d e . C e n t e r . X - p w / 2 ,   i n s i d e . T o p - p h ,   p w ,   p h ) ;  
  
 	 	 	 	 	 c a s e   T y p e . P a d d i n g B o t t o m :  
 	 	 	 	 	 	 b o x   =   t h i s . o b j e c t M o d i f i e r . G e t F i n a l P a d d i n g ( t h i s . o b j ) ;  
 	 	 	 	 	 	 r e t u r n   n e w   R e c t a n g l e ( i n s i d e . C e n t e r . X - p w / 2 ,   i n s i d e . B o t t o m ,   p w ,   p h ) ;  
  
 	 	 	 	 	 c a s e   T y p e . G r i d C o l u m n :  
 	 	 	 	 	 	 b o x   =   t h i s . o b j e c t M o d i f i e r . G e t G r i d C e l l A r e a ( t h i s . o b j ,   t h i s . c o l u m n O r R o w ,   0 ,   1 ,   1 ) ;  
 	 	 	 	 	 	 b o x . B o t t o m   =   e x t . T o p + d ;  
 	 	 	 	 	 	 b o x . H e i g h t   =   h ;  
 	 	 	 	 	 	 r e t u r n   b o x ;  
  
 	 	 	 	 	 c a s e   T y p e . G r i d R o w :  
 	 	 	 	 	 	 b o x   =   t h i s . o b j e c t M o d i f i e r . G e t G r i d C e l l A r e a ( t h i s . o b j ,   0 ,   t h i s . c o l u m n O r R o w ,   1 ,   1 ) ;  
 	 	 	 	 	 	 b o x . L e f t   =   e x t . L e f t - d - h ;  
 	 	 	 	 	 	 b o x . W i d t h   =   h ;  
 	 	 	 	 	 	 r e t u r n   b o x ;  
  
 	 	 	 	 	 c a s e   T y p e . G r i d W i d t h :  
 	 	 	 	 	 	 b o x   =   t h i s . o b j e c t M o d i f i e r . G e t G r i d C e l l A r e a ( t h i s . o b j ,   t h i s . c o l u m n O r R o w ,   0 ,   1 ,   1 ) ;  
 	 	 	 	 	 	 b o x . L e f t     + =   t h i s . o b j e c t M o d i f i e r . G e t G r i d C o l u m n L e f t B o r d e r ( t h i s . o b j ,   t h i s . c o l u m n O r R o w ) ;  
 	 	 	 	 	 	 b o x . R i g h t   - =   t h i s . o b j e c t M o d i f i e r . G e t G r i d C o l u m n R i g h t B o r d e r ( t h i s . o b j ,   t h i s . c o l u m n O r R o w ) ;  
 	 	 	 	 	 	 b o x . B o t t o m   =   e x t . T o p + d - h ;  
 	 	 	 	 	 	 b o x . H e i g h t   =   h ;  
 	 	 	 	 	 	 r e t u r n   b o x ;  
  
 	 	 	 	 	 c a s e   T y p e . G r i d H e i g h t :  
 	 	 	 	 	 	 b o x   =   t h i s . o b j e c t M o d i f i e r . G e t G r i d C e l l A r e a ( t h i s . o b j ,   0 ,   t h i s . c o l u m n O r R o w ,   1 ,   1 ) ;  
 	 	 	 	 	 	 b o x . B o t t o m   + =   t h i s . o b j e c t M o d i f i e r . G e t G r i d R o w B o t t o m B o r d e r ( t h i s . o b j ,   t h i s . c o l u m n O r R o w ) ;  
 	 	 	 	 	 	 b o x . T o p         - =   t h i s . o b j e c t M o d i f i e r . G e t G r i d R o w T o p B o r d e r ( t h i s . o b j ,   t h i s . c o l u m n O r R o w ) ;  
 	 	 	 	 	 	 b o x . L e f t   =   e x t . L e f t - d ;  
 	 	 	 	 	 	 b o x . W i d t h   =   h ;  
 	 	 	 	 	 	 r e t u r n   b o x ;  
  
 	 	 	 	 	 c a s e   T y p e . G r i d W i d t h M o d e :  
 	 	 	 	 	 	 b o x   =   t h i s . o b j e c t M o d i f i e r . G e t G r i d C e l l A r e a ( t h i s . o b j ,   t h i s . c o l u m n O r R o w ,   0 ,   1 ,   1 ) ;  
 	 	 	 	 	 	 b o x . L e f t   =   b o x . C e n t e r . X - h / 2 ;  
 	 	 	 	 	 	 b o x . W i d t h   =   h ;  
 	 	 	 	 	 	 b o x . B o t t o m   =   e x t . T o p ;  
 	 	 	 	 	 	 b o x . H e i g h t   =   h ;  
 	 	 	 	 	 	 r e t u r n   b o x ;  
  
 	 	 	 	 	 c a s e   T y p e . G r i d H e i g h t M o d e :  
 	 	 	 	 	 	 b o x   =   t h i s . o b j e c t M o d i f i e r . G e t G r i d C e l l A r e a ( t h i s . o b j ,   0 ,   t h i s . c o l u m n O r R o w ,   1 ,   1 ) ;  
 	 	 	 	 	 	 b o x . B o t t o m   =   b o x . C e n t e r . Y - h / 2 ;  
 	 	 	 	 	 	 b o x . H e i g h t   =   h ;  
 	 	 	 	 	 	 b o x . L e f t   =   e x t . L e f t - h ;  
 	 	 	 	 	 	 b o x . W i d t h   =   h ;  
 	 	 	 	 	 	 r e t u r n   b o x ;  
  
 	 	 	 	 	 c a s e   T y p e . G r i d M a r g i n L e f t :  
 	 	 	 	 	 	 b o x   =   t h i s . o b j e c t M o d i f i e r . G e t G r i d C e l l A r e a ( t h i s . o b j ,   t h i s . c o l u m n O r R o w ,   0 ,   1 ,   1 ) ;  
 	 	 	 	 	 	 b o x . B o t t o m   =   e x t . T o p + d - h ;  
 	 	 	 	 	 	 b o x . H e i g h t   =   h ;  
 	 	 	 	 	 	 l   =   S y s t e m . M a t h . M a x ( e ,   t h i s . V a l u e ) ;  
 	 	 	 	 	 	 b o x . L e f t   + =   t h i s . o b j e c t M o d i f i e r . G e t G r i d C o l u m n L e f t B o r d e r ( t h i s . o b j ,   t h i s . c o l u m n O r R o w ) ;  
 	 	 	 	 	 	 b o x . L e f t   - =   l ;  
 	 	 	 	 	 	 b o x . W i d t h   =   l ;  
 	 	 	 	 	 	 r e t u r n   b o x ;  
  
 	 	 	 	 	 c a s e   T y p e . G r i d M a r g i n R i g h t :  
 	 	 	 	 	 	 b o x   =   t h i s . o b j e c t M o d i f i e r . G e t G r i d C e l l A r e a ( t h i s . o b j ,   t h i s . c o l u m n O r R o w ,   0 ,   1 ,   1 ) ;  
 	 	 	 	 	 	 b o x . B o t t o m   =   e x t . T o p + d - h ;  
 	 	 	 	 	 	 b o x . H e i g h t   =   h ;  
 	 	 	 	 	 	 l   =   S y s t e m . M a t h . M a x ( e ,   t h i s . V a l u e ) ;  
 	 	 	 	 	 	 b o x . R i g h t   - =   t h i s . o b j e c t M o d i f i e r . G e t G r i d C o l u m n R i g h t B o r d e r ( t h i s . o b j ,   t h i s . c o l u m n O r R o w ) ;  
 	 	 	 	 	 	 b o x . L e f t   =   b o x . R i g h t ;  
 	 	 	 	 	 	 b o x . W i d t h   =   l ;  
 	 	 	 	 	 	 r e t u r n   b o x ;  
  
 	 	 	 	 	 c a s e   T y p e . G r i d M a r g i n T o p :  
 	 	 	 	 	 	 b o x   =   t h i s . o b j e c t M o d i f i e r . G e t G r i d C e l l A r e a ( t h i s . o b j ,   0 ,   t h i s . c o l u m n O r R o w ,   1 ,   1 ) ;  
 	 	 	 	 	 	 b o x . L e f t   =   e x t . L e f t - d ;  
 	 	 	 	 	 	 b o x . W i d t h   =   h ;  
 	 	 	 	 	 	 l   =   S y s t e m . M a t h . M a x ( e ,   t h i s . V a l u e ) ;  
 	 	 	 	 	 	 b o x . T o p   - =   t h i s . o b j e c t M o d i f i e r . G e t G r i d R o w T o p B o r d e r ( t h i s . o b j ,   t h i s . c o l u m n O r R o w ) ;  
 	 	 	 	 	 	 b o x . B o t t o m   =   b o x . T o p ;  
 	 	 	 	 	 	 b o x . H e i g h t   =   l ;  
 	 	 	 	 	 	 r e t u r n   b o x ;  
  
 	 	 	 	 	 c a s e   T y p e . G r i d M a r g i n B o t t o m :  
 	 	 	 	 	 	 b o x   =   t h i s . o b j e c t M o d i f i e r . G e t G r i d C e l l A r e a ( t h i s . o b j ,   0 ,   t h i s . c o l u m n O r R o w ,   1 ,   1 ) ;  
 	 	 	 	 	 	 b o x . L e f t   =   e x t . L e f t - d ;  
 	 	 	 	 	 	 b o x . W i d t h   =   h ;  
 	 	 	 	 	 	 l   =   S y s t e m . M a t h . M a x ( e ,   t h i s . V a l u e ) ;  
 	 	 	 	 	 	 b o x . B o t t o m   + =   t h i s . o b j e c t M o d i f i e r . G e t G r i d R o w B o t t o m B o r d e r ( t h i s . o b j ,   t h i s . c o l u m n O r R o w ) ;  
 	 	 	 	 	 	 b o x . B o t t o m   - =   l ;  
 	 	 	 	 	 	 b o x . H e i g h t   =   l ;  
 	 	 	 	 	 	 r e t u r n   b o x ;  
  
 	 	 	 	 	 c a s e   T y p e . G r i d C o l u m n R e m o v e :  
 	 	 	 	 	 	 b o x   =   t h i s . o b j e c t M o d i f i e r . G e t G r i d C e l l A r e a ( t h i s . o b j ,   t h i s . c o l u m n O r R o w ,   0 ,   1 ,   1 ) ;  
 	 	 	 	 	 	 b o x . B o t t o m   =   e x t . T o p + d ;  
 	 	 	 	 	 	 b o x . H e i g h t   =   h ;  
 	 	 	 	 	 	 b o x . L e f t   - =   1 0 * 2 ;  
 	 	 	 	 	 	 b o x . W i d t h   =   1 0 ;  
 	 	 	 	 	 	 r e t u r n   b o x ;  
  
 	 	 	 	 	 c a s e   T y p e . G r i d C o l u m n A d d B e f o r e :  
 	 	 	 	 	 	 b o x   =   t h i s . o b j e c t M o d i f i e r . G e t G r i d C e l l A r e a ( t h i s . o b j ,   t h i s . c o l u m n O r R o w ,   0 ,   1 ,   1 ) ;  
 	 	 	 	 	 	 b o x . B o t t o m   =   e x t . T o p + d ;  
 	 	 	 	 	 	 b o x . H e i g h t   =   h ;  
 	 	 	 	 	 	 b o x . L e f t   - =   1 0 ;  
 	 	 	 	 	 	 b o x . W i d t h   =   1 0 ;  
 	 	 	 	 	 	 r e t u r n   b o x ;  
  
 	 	 	 	 	 c a s e   T y p e . G r i d C o l u m n A d d A f t e r :  
 	 	 	 	 	 	 b o x   =   t h i s . o b j e c t M o d i f i e r . G e t G r i d C e l l A r e a ( t h i s . o b j ,   t h i s . c o l u m n O r R o w ,   0 ,   1 ,   1 ) ;  
 	 	 	 	 	 	 b o x . B o t t o m   =   e x t . T o p + d ;  
 	 	 	 	 	 	 b o x . H e i g h t   =   h ;  
 	 	 	 	 	 	 b o x . L e f t   =   b o x . R i g h t ;  
 	 	 	 	 	 	 b o x . W i d t h   =   1 0 ;  
 	 	 	 	 	 	 r e t u r n   b o x ;  
  
 	 	 	 	 	 c a s e   T y p e . G r i d R o w R e m o v e :  
 	 	 	 	 	 	 b o x   =   t h i s . o b j e c t M o d i f i e r . G e t G r i d C e l l A r e a ( t h i s . o b j ,   0 ,   t h i s . c o l u m n O r R o w ,   1 ,   1 ) ;  
 	 	 	 	 	 	 b o x . L e f t   =   e x t . L e f t - d - h ;  
 	 	 	 	 	 	 b o x . W i d t h   =   h ;  
 	 	 	 	 	 	 b o x . B o t t o m   =   b o x . T o p + 1 0 ;  
 	 	 	 	 	 	 b o x . H e i g h t   =   1 0 ;  
 	 	 	 	 	 	 r e t u r n   b o x ;  
  
 	 	 	 	 	 c a s e   T y p e . G r i d R o w A d d B e f o r e :  
 	 	 	 	 	 	 b o x   =   t h i s . o b j e c t M o d i f i e r . G e t G r i d C e l l A r e a ( t h i s . o b j ,   0 ,   t h i s . c o l u m n O r R o w ,   1 ,   1 ) ;  
 	 	 	 	 	 	 b o x . L e f t   =   e x t . L e f t - d - h ;  
 	 	 	 	 	 	 b o x . W i d t h   =   h ;  
 	 	 	 	 	 	 b o x . B o t t o m   =   b o x . T o p ;  
 	 	 	 	 	 	 b o x . H e i g h t   =   1 0 ;  
 	 	 	 	 	 	 r e t u r n   b o x ;  
  
 	 	 	 	 	 c a s e   T y p e . G r i d R o w A d d A f t e r :  
 	 	 	 	 	 	 b o x   =   t h i s . o b j e c t M o d i f i e r . G e t G r i d C e l l A r e a ( t h i s . o b j ,   0 ,   t h i s . c o l u m n O r R o w ,   1 ,   1 ) ;  
 	 	 	 	 	 	 b o x . L e f t   =   e x t . L e f t - d - h ;  
 	 	 	 	 	 	 b o x . W i d t h   =   h ;  
 	 	 	 	 	 	 b o x . B o t t o m   - =   1 0 ;  
 	 	 	 	 	 	 b o x . H e i g h t   =   1 0 ;  
 	 	 	 	 	 	 r e t u r n   b o x ;  
  
 	 	 	 	 	 c a s e   T y p e . G r i d C o l u m n S p a n I n c :  
 	 	 	 	 	 	 r e t u r n   n e w   R e c t a n g l e ( e x t . R i g h t - h ,   e x t . B o t t o m - h ,   h ,   h ) ;  
  
 	 	 	 	 	 c a s e   T y p e . G r i d C o l u m n S p a n D e c :  
 	 	 	 	 	 	 r e t u r n   n e w   R e c t a n g l e ( e x t . R i g h t - h * 2 ,   e x t . B o t t o m - h ,   h ,   h ) ;  
  
 	 	 	 	 	 c a s e   T y p e . G r i d R o w S p a n I n c :  
 	 	 	 	 	 	 r e t u r n   n e w   R e c t a n g l e ( e x t . R i g h t ,   e x t . B o t t o m ,   h ,   h ) ;  
  
 	 	 	 	 	 c a s e   T y p e . G r i d R o w S p a n D e c :  
 	 	 	 	 	 	 r e t u r n   n e w   R e c t a n g l e ( e x t . R i g h t ,   e x t . B o t t o m + h ,   h ,   h ) ;  
  
 	 	 	 	 	 c a s e   T y p e . C h i l d r e n P l a c e m e n t :  
 	 	 	 	 	 	 r e t u r n   n e w   R e c t a n g l e ( e x t . R i g h t ,   e x t . B o t t o m - h ,   h ,   h ) ;  
  
 	 	 	 	 	 d e f a u l t :  
 	 	 	 	 	 	 r e t u r n   R e c t a n g l e . E m p t y ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
  
 	 	 p u b l i c   s t a t i c   r e a d o n l y   d o u b l e 	 	 m a r g i n   =   3 8 ;  
 	 	 p r o t e c t e d   s t a t i c   r e a d o n l y   d o u b l e 	 a t t a c h m e n t T h i c k n e s s   =   2 . 0 ;  
 	 	 p r o t e c t e d   s t a t i c   r e a d o n l y   d o u b l e 	 a t t a c h m e n t S c a l e   =   0 . 3 ;  
  
 	 	 p r o t e c t e d   E d i t o r 	 	 	 	 	 e d i t o r ;  
 	 	 p r o t e c t e d   O b j e c t M o d i f i e r 	 	 	 o b j e c t M o d i f i e r ;  
 	 	 p r o t e c t e d   P a n e l s C o n t e x t 	 	 	 	 c o n t e x t ;  
 	 	 p r o t e c t e d   W i d g e t 	 	 	 	 	 o b j ;  
 	 	 p r o t e c t e d   T y p e 	 	 	 	 	 	 t y p e ;  
 	 	 p r o t e c t e d   i n t 	 	 	 	 	 	 c o l u m n O r R o w ;  
 	 	 p r o t e c t e d   b o o l 	 	 	 	 	 	 s l a v e ;  
 	 }  
 }  
 