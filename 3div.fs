#version 460 core
in vec2 TexCoords;
uniform sampler2D uv0;//tex0
uniform float gridsize;
out vec3 div;//tex1 (div,curl,?)
//dx=1m
//this shader is to compute the divergence of the velocity field,
void main()
{
	float sandu=0.0;
	float curl=0.0;
	//float s=0.0;
	//zuo
	vec3 oldpos=vec3(gl_FragCoord.x-1.0,gl_FragCoord.y,gl_FragCoord.z);
	vec2 oldtexcoord=vec2((oldpos.x-0.5)/799.0,(oldpos.y-0.5)/599.0);

	vec3 uv=vec3(texture(uv0,TexCoords));
		//zuo
		float wl=0.0;
		float cl=0.0;
		oldpos=vec3(gl_FragCoord.x-1.0,gl_FragCoord.y,gl_FragCoord.z);
		//oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/600.0);
		oldtexcoord=vec2(oldpos.x/gridsize,oldpos.y/gridsize);
		//oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/800.0);
		vec3 L=vec3(texture(uv0,oldtexcoord));
		wl=L.x;
		cl=L.y;
		if(oldpos.x<0)
		{
			wl=-uv.x;
			//wl=-4.0; //to test the compute on boundary
		}
		//div(R.x-L.x+T.y-B.y)

		//you
		oldpos=vec3(gl_FragCoord.x+1.0,gl_FragCoord.y,gl_FragCoord.z);//you
		float wr=0.0;
		float cr=0.0;
		//oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/600.0);
		oldtexcoord=vec2(oldpos.x/gridsize,oldpos.y/gridsize);
		//oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/800.0);
		vec3 R=vec3(texture(uv0,oldtexcoord));
		wr=R.x;
		cr=R.y;
		
		if(oldpos.x>gridsize)
		{
			wr=-uv.x;
			//wr=4.0; //to test the compute on boundary
		}

		//shang
		oldpos=vec3(gl_FragCoord.x,gl_FragCoord.y+1.0,gl_FragCoord.z);
		float wt=0.0;
		float ct=0.0;
		//oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/600.0);
		oldtexcoord=vec2(oldpos.x/gridsize,oldpos.y/gridsize);
		//oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/800.0);
		vec3 T=vec3(texture(uv0,oldtexcoord));
		wt=T.y;
		ct=T.x;
		
		if(oldpos.y>gridsize)
		{
			wt=-uv.y;
			//wt=4.0; //to test the compute on boundary
		}
	

		//xia
		oldpos=vec3(gl_FragCoord.x,gl_FragCoord.y-1.0,gl_FragCoord.z);
		float wb=0.0;
		float cb=0.0;
		//oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/600.0);
		oldtexcoord=vec2(oldpos.x/gridsize,oldpos.y/gridsize);
		//oldtexcoord=vec2(oldpos.x/800.0,oldpos.y/800.0);
		vec3 B=vec3(texture(uv0,oldtexcoord));
		wb=B.y;
		cb=B.x;
		
		if(oldpos.y<0)
		{
			wb=-uv.y;
			//wb=-4.0; //to test the compute on boundary
		}
		

	sandu=(wr-wl+wt-wb)*0.5;
	curl=(cr-cl-ct+cb)*0.5;
	div=vec3(sandu,curl,0.0);//the correct one for final version
	//div=vec3((sandu+1.0)*0.5,0.0,0.0);//to test divergence compute is correct on boundary
	//div=vec3(0.2,0.0,0.0);


}