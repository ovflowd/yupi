using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using FluentMigrator;

namespace
Yupi.Model.Db.Migrations
{
	[Migration(00000001)]
	public class Initialize : Migration
	{

	public const string ConfigurationData = "H4sIAAAAAAAAA+3db3ObSKLv8efzKlxT9+HUlP5a0nkmEExca098bCfZuampXSK1ZW4QaAE58W7te7+NhDH6gyJZkoemvzm1JxqLYDXqz4/upmn+89PZ2c//J36aip//5+zn26coFpNfzcDzxDB2Az/69Tfhi9Ad/nrpRvE/658/295M+PGVOw6dOAh/tb5PQxFF800XP5QvX35oOJH45Wz53/z55y9nk2gYhJ775edf5r//0ZFbRPITfJb/eXb2n/n/z3+wLb/VDIUTizvniydefrz6O+e/Z77L2+GDmDi/O5Nkv/7M87J35rtI3/i5P3xwxaOYyH28/Ft5XGYTP/mcz5/wsIN3FYyE9+tirwNx7/puvOGzrx2v51+8ctQWf/6Te73lCO72q3O/br63fzij0XxDx7PlUZ+FYvlg7HFIBu78B0749M/G58/phrdx6Prjl+L++cvzO++//D+5h9w7+YOy9Pv/u/KZn7/Ri9Fqae4Wn7JeX/n5rfvv5Oe1lR9fh2LoJpVrw3vmLIqDSbrDfK1K35eH2Jl58cfkG9u8xUVkB6Fwx/7fxJPc4t7xpJzVTS5G8stx42SDOJxteN8fie9iVPjvr0N3Io/44lds2MPL+xuAZHv5Xf4wsVL4az747r9mRW//kFm6nayh7r07nAdKemDXjnpafaNh6E7jxTez4RMn1W++m+JC9RWo17n/WqrjiC8Qb8rzwjgIn4rcn29232i3Sy2/YIN96G/aZG/7GxMI+tAvAf3fwmA2nf8H9rFfGfvZ6z/TV1n9Xxzf9WPyU26rcnWrLuXfHn2rbG8qxy19q3L3rZatpRuTtLSyDmO/sVopLb967SvoQ/8E9G/Ev2ZuuLEFTwAQAEubEACVDIBvTjjqy9/6KOVcB64fRyQBSUASkARxNK9JDL2SB+SBpnlAiyB9nwQgATRLgFwNuxH3ZAAZUKkMyF4rexX20Ymd0Lq/l6XnCmy2N5UjlyuwZbwCu8lZuiEJSyvr0FZW7MoKJorkN1WEX5X2FfKRf3r5/aKLrwVjrNjHfrlqOPb3tL+oVxVr7iP/DPnpH+QXyL8LYscbzBa3WMMf/vDXiv+WKRWrVQn1qC9l3Ub9q5r7ZjCZBr4s4odIhCs/+odLX4BYqE4sZK9VvbpqOKOx4LJqtjeV47di4yzVuKy6DCzdgkylqXXgckGyxMxZL2NbCvCAPwH4Wy/g5lW4w10P7vNqFSUjKPNX2QAKU9RJgUqkQAVGTiJxIQ8AgyfZ3lQOXAZPSjl4smIs3YhkpX11GPeRm1SMietvKhgDKUdpU6Ef/eXU3x99CAvXAkT9ciHfuicFetCfBL3nBd8u/EdZvCB8uo2d4VduRsM//nXyf+WEX0U89ZyhuBVeURuAAMgXkgAoZTUnAF4TADdi+DT0iuZOAD9fSOCXsnoD/zXw70KncMoU7POFhH0pKzfs950j6TlR5PNctTPgA18n+AN3IvyE698rdU0f97hP/uD+R+7/wD3uca+Le3sW+u6l69PQBz7wNYL/TmJ6KLotqsO5PldIyJeyYkN+T/I34nGhlRY+6lGvifr5bL15Jd3Mngt4+UKivpR1G/X7jue9VAwWjsM+9jWyn/wNetCDXiP0KbmBW3hLLu7zhcR9KWs37vddfMOPRSg/+LUTxvIDBWHhs7g38+dCHgGw9IcAKOmRKQiAOXrO+ZCHvC7kbwN/TAcf9KDXCP1NEEyStUxF7DKq//w+/qvgP3ut6oKmphM7XjB+f38vcv+YRU0VjlsWNS3hoqYbnaUbkrC0sFjYdGmTEjavSAASoARHpiAB+vL3PUow14Hrx9G8DhEDJexlkQKkwInXPPrNvS+6O4oZ0/lCIr+09Rv5e8rf+CwiTvnAz20C/ArCN73Zl0vxKHiyAfjBrxv+IIqXe/6VGvonA87IgPQPGbAlA8xQyM8PfvCDXyv8F9GNLBlrJEAf+trR/yjFIh/5yNdL/u883eAM97jXzX2+YsAf/vDXir/1ferKz9svmtxTYJ8GP/LLVb+Rv29Xf8gZH/e41879xGFSH/CBrxv861k4fHAicelO3KLmPlf1lwoJ/9JWcvjvu0pS7MTCDAqfXM65f6WQ4C9tFQf/vvP5FrXrWjb9WSkJ/1Xyn71WfLWk63y3nMWSVE5bFksq72JJ1xtGv8hX2lenu5iipPlqtapAD/qT3CcRBKyKgnrUa6T+yvVvHP8rZ3vc414j99uWPwR9vpCgL2vVBv2e6LkbMn0f9rDXh73p8Kj47H3oQ18f+pfOUzCLkY985Osl/1Ykn0GM5lNwCAACgADQKwCYJLnyPvwrwj97XYE5kosGOjMls72pHLnMlCz3TMkVbOnWZC1NLZ4tubRJCdtaxAAxUJYjUxAD74QzEiErUjy/X+YOFxlABpxkXpVwIjLg5X0ygAzQLQPMwI9l0Rh1JQAIAB0D4GMwGz6IMFc/yAKygCzQMQtup9Kt49EjeH6fECAEdAuB3KhAnRAgBAgBvUOgQQgQAoSA3iHQJAQIAUJAwxBYTBRgeHDlfaKAKNAtCu7Edy4Uoh/92uqvwx/+8NeVfwP+8Ie/rvyb8Ic//HXl34I//OGvK/82/OEPfx35c/Vv5X1CgBDQLQQsnxBYep8QIAR0C4H0DiFmAjy/TwhUKgSy16ov1xgGo9mQtRpf9qZy6rJWY4nXalyRlm5KytLUYqHGpU1K2NYiA8iAUhyZggzoT4KZX/ScazVP/mWhf6RuFvKRf5Imv9wNIyzQh7529C9loz8WoyQBoktxz+mfDCAD9MyAW/nbRTSHTQgQAoSAViGQ1i+eipm9TwRUJwKy18pean1w4iu5r/xiqVxnVTlwuc5axuusG5il25GvNLEOE2/MviS1tErqq9Kygj3sT8W+oGIxqwL5L5sgv4Ly79yJiGJnMi2wX0CfUz7wz5b+AL+kR6YA/qcHN5qKoomUTdjnCgn7slZu2O/J/kPEtZMz3ONeM/c3QTAZOLGDfexXyH72WtWLptYwiMPAvxSPwuOqabY3laOWq6YlvGq60Vm6IQmrQcLeiG9OOCJis70RsUTsKSJ2BVq6JRlLD/bAqSlOJLgL8Pn90vVg0Y/+U662mGu/kwAkQKUSQPk+lh26wh/dCFlTIhZZe9mbyolLH6uEfazN0NItyVhaWYeZt8OA/lU5W1fIR/4p5wEHuMc97nVzfyMWX3v04E6j/H+YwWQa+LLU5AK5UKlcUH605bcwmE0ZZcn2pnL+MspSwlGWZWDpFmQqba0DV68eTVz/ve89DcQwqBT7irSpcI/7k8xcGbG2AuIRr434tN/nTlhKCfSg1wN9vmJwssc97vVwP/8b8IAHvB7gb2PZuKdhj3e8a+FdfohgFtaZCAF60OuFvgF60INeG/TJuH3AAomgB7026KWlGXc9QR7y2pBPVkNFPOIrIT57rfT9DIYz/Jq88EeLnnfEDQ7Z3lSOWm5wKOsNDsXi0n9C6tLOOsYgaqUCoErNLBKAdleyrCYtriqFLi2u8ra41q2lG5O0tLVO2dZiIupyIf+S1hb69W5n0cJ62ZvKUUsLq8wtLNKVttWRwc+x1WlblbpthXvcn8J9A/e4r4z7avSm5nP26E1le1M5ZOlNlbU3taws3Yx0pVV1hAnXrJay/H4521ckAAlwqgRg3ZT0fehDXy/6t/JfFNnvqtjoBz7wF3+AXwD/00NgOr4sd6V6+8AH/uIP8LfCvw7WH1OEfOSnmyC/svJvhMM5H/nI107+3UOIfexXx372Wv3JE0vtcSZQqBy2TKAo9QSKTT1fUpYW1qG3+/mxLBoXUMvdvAI/+I+P/507kmYK7DdVPOkDPz3ywD8DfvGjZ2bzXXDWBz/4dcOfPE82ip3J2vKoaXUq0M+JH/tlq+HYf1WL33hi1Wr4w187/kmtEjyXBvzg1w//S/1aTKggBoiBasVA9lr9eRUrc56YWaFy8jKzotQzKzbPLyRpaXAd4TGgYtQvus7CQGu+kH9VKwv96GdyBfSzjaEP/cPoXwbDr6KozQ/9fCGhX+oKDv19r7C4vg996ENfO/pMqnx5H/7w14z/fLQvYHIF/vGvo3+mVubfJwAIAM0C4KWGEQFEQNUiIHut9Nyq26fJl8AzeVB9hXKX2VVlnV21WVu6OWlLg+uUj6pXEn+VWlrop60VeLSyXvamctjSyip3K4uEpX11ZPI8rj57v8wtLOQj/xTyeWA98qskX/le1TvH897f2/K/LE9M8qv20rVSOW3pWpWwa1WMLd2arKWVdej49WQq4vmnp6lVxqYWGUAGnPqmgWHxI6zVPP1j/+XgY/8M+4X2P0SsxXiG/urpV3+kJYiFd+n4I3lcrhzfGYuQsZZsbypnLmMtZRxr2cIt3Z68pbV1mHx7FvrujfjmhKx8W9JGFzFAs+u5AlyHwSRg8uDL3lTOXlpdJW91rWhLNydtaXQdBt8IRk9c2yp7Ywv96D+J/lkcc20b//jX0z8L4Gfv4x//2vm/mDjjwsktnP6XCwn/kldy+O/J/86NPfjDH/5a8r90/a8fQo8AIAAIAB0DYMP1fGZekAbVS4PstaoTL5JTNVMtsr2pHLpMtSjhVIslX+kGJCrtqwOpSwixCOVH53ljpWtSQR7yJxhSFd95wBje8a6J9w83l3CHO9z14G4+OPGViCJnLBgpxX0V3GevVR0bTTwKfyzCFCbjpNneVI5axklLOE5aaC3dmKSlhXUY+5sNz/1jtPQvb1YBH/iMnGIf+9g/xUT0iYhiZzLlPjT0o18z/R/8e9eLRShGtAHS90kBUkCzFLDDYMI1FfCDX0P8dwH0oV81+tlrZS+tyuiZOK7HJdVsbyqnLJdUy3hJddVYuhHJSqPqwEdXCVG0th+XUvOFfOMmFeABf5qB1EiEF/49fSngVwa++n0oGUPh4rsVk6n8epig+rI3ldOW3lQZe1PF2tLNSVuaWYfB7z86sRMaDp2rErax8I//E/uX8t8Fs3BtCa7n0/9qdcI//ktcy/G/v/+Cq6HMVSMDyAAdMsB0FpWCACAACAAtAyAW4yAsfHginYB8IfFf8lqO/339z19w+oc//DXkfzVbr1UM/0Mf+pWnfxc6I3EZDNeWmcM//vFfef+fnNBPHpjEJYDsfXKgWjmQvVZ30mUw8tzxQzxwYof5ltneVM5d5luWcr7lJmjplmQsba3DzFt+UtlYELSMLSzgA/90l1dmYSiLJulGIuZ+NiKgUhFQnf7VAig9rGxvKocuPawy97BWqKXbkrM0tQ6dzj78Og6DmT9673tF89noauUL+Re1s0gAEuAkna3AC0JTFpuLWfjHv57+k0fV+1ECp0p9ACLg+dATAWdEQPF81vxYHuOthEDFQiB7reqI6+/OoztO/uXajWeMuaocvIy5lnDMtRhbujVZS4PrMPcjN6kYE9ffVDDGXY7S3iIGiIGyHJmioRfWEcneL12HC//4P3XzP7qYFN9FxFXXfCHBX+4qDv698b+fyt/N9Hb0o187/Veuf+P4RasIqDn8h/6Xg4/+M/QX6v8o0SZ1lVM/+MGvGf61KnYj7mkHEAXVioLstapzLq7loWOaRbY3lROXaRYlnGax5CvdgESlcXUgdf/RjR2uqqbvl65RhXrUH199UqtYkj19H/OY18F8mCAGPOABrwX4uwfH/1r43FXELxcS8aWr14jfU/xNEEy4HRXwVQCfvVb5esj/zkS0NLbGdRGV85XrIiW9LrLmLN2QhKVJdRj5vh99E+G87tCRKmm7Cv3oP9VaX6FEGy9CgAAgAAgAvQKgqGZhH/u5TbBfQftJ7WI0FfhVgp+9VnVU9UYsvpLowZ0yqprtTeWcZVS1hKOqG52lG5KwNK0OIz8QnohZyqOELSvc4/6EU9K2XEdR8mSP+jPUp39QX6DeDl3hjxhKgT70NaOfr11R/j/MYDINfFlqFvMgFqoUC9lrZUdYg2CS6BSxy9TVpb2pHMQMspZxkLWAWrotOUvz69Cl07jHv6TtK+jTxEq/fMuPeUJdbm8qJy7trPK3s5a9pf+AxKWxdeAi1UEsCpdbUFJ/Ndta8Ic/iy8QAAQAAXDsAMjVMrKALKhiFlRi9GXgxA4jLtneVI5dRlxKOuKyZCzdiGSllXXggiyeF3y7FnHRSAs3D+QL+fZtK9Sj/oTqLfnt+2PsYx/7Otm/cccPcfT+UYShO+JBoPjHv1b+Pzne17uHMJiNH8APfvBrgt8073iEBeYxr5H5fMWAPvShrw192wvWC5RWpQ4N/FwhEV/Keo3414i/e3CHX30RMX0a+9jXxv47dyQ+OesPRmNED/RnoK8m+ovoasban5jHvD7mLx1/dDt0Chf/pGufLyTqS1m3Ub+n+nmJ6dFDHvK6kGfBqfR9zGNeG/OP7jgp1MXEGaMf/ejXSP+1E0XfgrDwJlzcLxcS96Ws3bjf0/2t3Jhn+UAe8vqQj50Y8pCHvDbk70JnJHB/hnvca+X+qfBaPT36lUJivpQ1G/N7mv8QiTC6cr5zpkc96nVRn8y/fyeS1XVwj3vc6+T+2pkK7rZFPep1Us/Nti/vQx/6mtA3nViMg/CJ55IAH/gawbceZcFQj3rUa6T+tzCYTVGPetRrpP79N/mLUY961Guk3nxwYsPxvKB4wVzg5wsJ/FJWb+C/An6ygOboOgzixachAAgAAkCnALhyvg/cKHb8IZP0wQ9+rfDfTkXhWpqwXyok7EtZuWH/Cvbb7s9Bfb6QqC9l3Ub9KxbTDOdf/K2Ik2dgR58eAtPxDYcuP0FAEGgfBH9zh19JApKAJNA9CZLna5AEJEElkiB7/Wf6KtOwOLjrx+Sn3FbPBStMDev7VB6CaF4wM5RHRMz3+vLjwgD5+Xb4ICbOhiO9/q3Pp+C+/MvFt7l84Hc65JduFP+z/vnzq6Jv6YD/kvvFj4nU5NN8JnVzNa8gdS+qNdy6MdP2itYNe9g7WTf/mv2idRlZuhXZSivrOPdNVUp9hVpUsIf9CdjnK0aBfBZCWy4k9stZw7G/7x2T36eu/Lz9oiWRCuBzyof90lawL+eRKWDPw8vS90FfCfTZa5WHTpcuZjByqnK+MnJa0pHTTRcMSVaaU/SizircoEI96k+gfi6FJShgD3uN2D/PbQI+8KsCX/nxk9vAHy9NOGT8ROWMZfykhOMna8bSjUhWmlSHce+HsQxdLkiVsT2FetSfaLqpLDEXotP3cY97TdxvqlWYx3y6CeYraP5S+OP44cr13EgMA3/EE7ngD39t+NPMT9/HfBXMZ6+VvVwy+7J+zxeXTFSOWC6ZlPGSySZn6YYkLK2qAy+byN/3mJwd3MLWFTNP84V846YV+MF/4jnn0Id+lehXoGc1nQZhfOcOvwrWQXvZm8pRS9eqlF2rTdDSLclYmleshXZW2dYV9KF/OvpeEIkb4UQ8YhL96NdN/7xbOOJefuhDXy/6VyKKnDETVbCPfd3s38qNeZQM7nGvmftYfuDC2eic8pcLCf3yVnDo70l/Xms448Me9jqxvxFJ7RKjD5EIWcOLBCABtEuAIJggH/nI103+rZAgOOtjH/v62Y+d+3voQ79S9LPXqt4wced4PDE+vzeVM5Y7JUp4p8SKsHQTUpUGFddPqtiYwjveT+Fd1uN/iwu5LzpRuK+G+4r0ni7Fo/DoQmV7Uzll6UKVtgu1zCzdjnylXXXo2sgbqpXS6CvVoEI96k+yeN+DK2vWRBaP/hT84a8X/0XluhH3yEd+VeSrP5wSOiNxGQy/MpiS7U3llGUwpYyDKWvI0q3IVlpVx1gUOWLxnjI2qmAP+9OwT27lu/DvAwZSkF8Z+VXoTPnR4luhO5XtTeWcpTtVzu7UGrN0O/KVltWB16YdfzxzxqJi8svSsMI+9v/6I1Ngf+GtgD0roy0XEvilrd7A3/fa9GLnjKaUdTQF+lqOpywOCWMp2d5UDtmK9agqMpayQizdhlylSUVfqqztKdDTmNq7MZVcMs5NwadRle1N5ZilUVXCRlURtXRbcpbG1aELUwTj5LxQKftlaV0dOFoFfvBzH+ViCyKACCACTrSAAvjBD3798DP3P/8+/ivkP3ut8iCr4TD7/2VvKictg6slHVzNE0u3IVdpV3Er9VmFG1WgB/3xT/HXzFEBPOC1AX/lyL65v+X2PtwvFxL3ZazduN/T/Y1wovX7R0AP+sUmoK8g+qRicZkE8xUxn71W+fKI6czmH5BLJM97UzlhuURS0kskq8zS7chX2lRMP8k2qWa7Cvuatq2SZ5XSsMr2pnLM0rAqacNqyVi6Ecla9WS9FU44fLgMWIzmZW/EK/F69Hhdh5ZuScbScz3CkjR1LgGWtdsKfeifkn4D+tCHvmb0Ga/Ov4/+yuivxLjK4sHDDKpke1M5ahlUKemgyoqydDPSlbbVYeBvY3laqJT5CrWqQA96Fp/C/WIz3OP+wIckzesV8IEPfK3gP4+g3oh76EO/IvSz16oOn35ywlEYfBFM+l3am8pJywBqCQdQNzpLNyRhaVwdRl7+4pFYKxEzU8rQskI+8k84jhoEX3GPe9zr5f7WC4qWmFWzmY/6M9Snf1Bf1LX3H2XJgvApe8FwKjFQpRjIXqs6oPpHMItnX8RHdyQCBlSzvSmduizzW8ox1Y3U0g0JWdpah6nPVwz4l7CNBX/4n47//G/c47467pXvWxlBnEwho1uV7U3lhGWeSgn7VKvE0m3IVdpTJ5yi0mjSoloq5Nu2qEAPemanQB7ykD/40YhBHK9VK8xjfrEJ5itonrHS9P3jk6c/j/lSmn//zeexiGec6KuCPnut6sUR2wsCnt1TnYDl8kgJL4+sI0u3IltpUB3mfeQmFWPi+psKRm+qFL0p+MOf/pR67g/sT8Ee9idibzijMWOnqEe9TuoXI0CjftEt/AXwOdnDfmkr2JfzyBSe7KP5HeE085GPfJ3kJxMhbTFxPNr60Ie+bvSvgH8GfOBrBZ9FebP3YQ97Xdhzz1P6PuhBrwv6G2fII0whD3l9yF+JKHK4do973GvlPnlSHhfwUI96ndRfe86T3Pl80cEt+jnrLxcS/+Ws5fjf9268tSdv0AYgAyqTAdlrVe9+vhYsDVuhtK1WtFbj3udVYuk25Cptq8O09/2nwBem49/I3lUB/IIFYsvtviJNKuAD/zT3Q8im4Eju/c71xD/+XnTKXx06gT70y1bBoX8I/T+gD33o60j//0If+tDXhL78EKxthnnM62SetU6e3wc96PVAb8nfO36q1EU8yEP+DPLF5L9P5S8WPjdCwR722rB/57iFPXrA5wsJ+DJWa8C/AvzgiZM85jGvj/lHcSs/f+EqZkzPyxcS9WWs26jfdyUjJ4rfCceLHxi6Rz3q9VD/+0zufF4taOGjHvV6qL/2nKEYXfg3QbB25yxtfNzjvqLug2j+0bn9Bvaw1489t97AHvbasee2G9jDXhf2rE4OeMBrBr5ia2hBHvJnkN9CPkygQB7ykNeEvIimcheYxzzmNTE/rzUF4FlAY7mQkC9jxYb8nuQ/OfKT+2OJ6nnlLM748Ie/Jvyvgjheq1ac8jG/2ATzFTQ//xvypyDPUzAwX0rz77/5PFj0jBM96DVCn9xug3nMV8R89lrlxwjGuWdH8hhBldO1YlOgqvMYwZjHs9KWOrZ2w4lEUrNoT5W2PYV73B//LC9b6ZjHPOb1Mc9gafY+6EGvB/oL/1GWLAifshckAAlQkQTIXqs6dPohEuHSgDljp0qHbbWStRpjp2vG0o1IVtpWh3HvT6fCCd/f33uuzyLv5WtSIR/5J7psEnz54tiuF4tQVOucD33oJ3+gX0B/0REcyP/xgAfYw14T9tbEcT1uLwE96PVBL3/xSBQ+qRX1y4VEfSnrNur3VP/OiezkoeyjGyHrYhRHAzdKal5RR58hvnwhSYFS1nVSYN8UcEeCRzudwR72OrFPnt96MaXJj3rU66X+vb/lMj6D+vlCwr6UlRv2+7IPgq+c6jGPeX3MX81iRvIQj3htxN84fuFZnjl6+UIivpT1GvF7ir+dOhPbC4LRnVu4Jiw9+nwhkV/K+o38veXLHSSFSopOKx/5yNdEPo98eH4f9KDXBD3PfEjfL98zH0AP+tOgt53HYBa6sfgtDGZTljPinA9/jfi/CyaCR0A8vw984GsC/3b2JasZ4Ac/+DXCb8xcb7SoXOnLyPo+dcPC7j8xkC8kMVDKyk4MHB4Dyaq50ZXznSAgCAgC3YNA1jh6BiQBSaBNElj393IPZjCZBr4sYl/++kex+CGjBGQBWaBRFkiu9yIU/lBE5oMTG7Mvstrdxk8ewwTkADmgYw6ki3eZ8p3QseWHC74xL5goIAr0i4KLsS9tJXMHLvxHt3ABb3IgX0hyoJS1nRx4fQ787jy646R87ySwh5iuATlADuicA5/cUfxADBADxICOMSC++TIJ/k4AEAAEgL4B8AcBQAAQABoGwOLle2+UXDRkVJAQIAT0C4GPSbFFnUYA/vGvrf8G/vGPf239N/GPf/zr4v9GRMmKoQPH9Z6SWwhEPC/JxyCWhSAJSAKSQMMkuBZx+p/XgevHJAFJQBJomQTEwPL7xAAxoFcMpH9hH/vY18X+J0d+iLg/fHDFo5jIQtIASN8nBAgBrULADIUsAfShD31t6D/XLBYQAn5V4Gev/0xfZZV/cXDXj8lPua2eC1YYEtb3qTwE0bxg8pzpxGK+15cfF+bFz7fDBzFxNhzp5W89OR8na/y9/MPFl7l83Hc64pduFP+z/vnzq4Ju6Xj/kvvFjwnU5NN8JmNzFa8gYy+qFa0bI22vZN2whzd/atOasXQjkpUm1WHcR25SMSauv6lgz/J5ZttyIdFfzjqO/j31v/8mfzG9qVL2plCP+tOoHzixA3rQg14j9IYTiaRmAR/4wNcIvvwQ9OxBD3qd0N+J74VzIzG/XEjMl7JmY35P87/PJl9E0Zm+S9s+V0jIl7JiQ37fi/X+oyxZED5lL+jeEwFViYDs9VtNjXqpmrvMj1qqyJtmOm2Mp5d/tVNEZdTtv3X7Zs9oG/J/dbtnWq38Vule79La+nPuHolL+bdXtO1ijtdadXn2sWl/PxdsVrSr97LueiJeqdryxx+mI2ftx+mH2zSFbMe6+TyJrKhWbp4mdlY0UexsqfA34j5fYf/MXv93/aiUqgwXo4LP/VP+76rSqVv1TtPuW522ZdqWNdhK59GJnXDxpNJD2WyYEauJmZXHvyYHYuVHSUsFSspRatf67abZbFvNttnodaxtlAxnNBYYem1Fmx++5NHp4fxVJoeTkJpyjM6g26jXGn2j12r3ze1yFldrD8Vzt/jW9LOT6xtwolGai9lrGMZ5rweXE1a15LDjRGknLaveqNtN+y2c3Ab+OJlFqB+UpORAURqK1W8bXfvceAsoyVONcwuY6udl5QBAR0k6HbPeOW/37Ua9a5n1bmcbHVOeF7xg/P7+fuli9Kv4pPu6dpaHEfSgkys8bJRkY1i9dts6twa9lmXY7doObFarOr2YvcTQ4VdfS68+MAZme2tH5ohacru6dJ6CmYYXOBflho3SbLpmyza7za3XNo/PZq2Zp4eYW5H8EjGaFx84SsPZo1Nz5PONfm7o06iuxrT7zfN+pzuw7Zph9LaOom1tWtGz2cdN4MfMmVHdTNtuGP2O0cfMW9S5j8FMli1kJkBV+DRaRqPVrDfh8xZ17p1wRuipjp7WoNeqte1dh9XQc1CdS27ABozaYMzOwKx1d71qA5iDwdQRUwExVh0xbyWmgZgKiDEbiHkrMU3EqC+m3mAQ4M3EtBBTATH21vvNEHNMMW3EKC1mlzs0EcOtmi9v4CZxY7etbtc0urh5kyU2fNxUw03HNuuDlsEowNvM3ZyKoet4XKFR1k27XW+0u/WaLf/XMeo7nW/CYDQ7fEGnTfeF6qHm+dkRcFGWS3Ngd+xua6cpm8fhovdNAvnSQ0dJOkav26j1a+3ewLatdn3rIjTmgxNfyQIc4R4Bfdc8S0qOFaWtdNoDq1fbfjvN8agkC0zouRTNc8nhoiSXbs/otGr9tt3snrfNwdZWmTUM4jDwb8Q3JxzRh3n9gpovz8BDjLJimo263e5s78ccVUy6s7WV1PVQky89cpSU02i2O2bTtvtGrdnvb53RbIeu8Ec34l/yMLL++avrmx0GnGaUxmK027VGvbX1fhmwHGfOTAAVlam0Gv1erT/Yeh0GKsfp9IvFU56iB3ca5f+D52qoTck2jH7XanTaLbNZazW3rhHwWxjMpocamu/k9mnyJfDk9xvMwkg/TYuCc9uZ2mSa9vnAMLdemzmeGMMZfk1e+CPN1XDrmdpq2oO2Ue+1tg4GHIWNvo21eXUIuJ6pNhTZvema/dbWuwGOd36R/3Cm4aWZebGBojaUrm3VrebWK/9HgaL3NX+UKKmk0z/vdBvnLaNh1VqtQfuHSuaReB0wWnbAGmbuaCR84wkySpOxTLPb6m1foRkyx6lxydFjCqbiYBrndq9nGltnyBwXzMve7h7kgRrpB2f1CEBISULN1vm5bfd3Hh0rqPOcdBgo04zMfh0byNC1wUz3vNXsNptbVzM7thmdh5lfyo4bJd30O812a3A+2OWGzHeO572/t+W/tTwxkbvmbMNtmdqJGdjdtmX3zxu2dd5vWVsnab4LYuFdOv5IHvIrx3fGhz/CWd+7zexZ6LuLG5CgoyYdq9vvN+xey+yZRre3dfw5T+c6DCZBdKicH2DUA9GGgwAmJTHVm+etTt/sD2o102qfb72v5tL1vx7Kp2CNAT3Y5AoPFyW5DIxuX7bYWrvcs5l808KX0cjyM9y2ueFzawVml/s2AcOtm5pzaQ+sZqvd6nesQbPWr22dKnAl9zxxXA8mh4yfJSUHi5JY6mZt0Og1m02zVm8NrO3nliAYee74IV6dofwaMdnOruUxEbF+cMxZGMqvZ1F89Cipp2XVG0bN6tbrsm3WtbZe7NxS4Q/yo+ftAkvFR4+Sevr1fq1ryHOP1T1vnW9dF+B359EdJ3syZanGQfh0KJ+tO9SD0NohuBH3MFKPkdmwzH5zp3vUruXXwS1q3KKmnZF22+50O71eu9a3eh3rh0j+N1nLSe79UCyr4PSAkpQaKEpCsRpWy+jUGs1uv1FvbF0ZIL9GF+Nnr78uk6wehxaVtQx6LaPZON860oyWt1kkkE6MkpK6UpE9qJu79GKSdnjyfYt4/gssPz58PIBeDWiURWP120bX/sH550RocvvU007uAMBISUYNuzE4H7QMqzfoWOYPOj0bzhOMP7/2IYLzkqNGaTX9ut0zuo3WW6hJ9mM9rtzvpoeWebGhojQVq9Fq1Vr1rdMEjkVlbTlCPZjMiw0TpZl0a7V2q90dvAUTfYfS3n/zueNMUSm9bqfZt1v1eve826798HxyNYu5EeD1LS9f9u7ppSgtZYcnNx8Lit4jyUzHVJaKZSQTMS2j265bfbuzdTmN29l0GoTxnTv8evhUZn1PLTciOYpixCI0yqvZ5cLlUdXofZ5Bi9JaOp2G0ZB/cY45eYW7Ff6Is4viXsx+vWk3rQZeTu8ldu7v4aIkl26z3ev122bjvN7vGdtXBLxzvCMsnKnvIoCylP+eFx0rSlox+rV6v9uyuvXaoHne2jqIvLByKR7FwbeP9YcPrtzP6qK1epjJFR41SqtpDYxuu9vY4QxzFDXrJys9wCzKzZx+Ja002u1esz7YZR2mu9AZictgePDamPr2W1iISWksnXqr1m2fG+2BWWtZ/R9p8aPFvU8Hn1gWX5x+XNKCo0VJLd1a0+rUOq1dOi9JMBb0OujA0IHRVE7drrf79tbFl08HZ61PpIeeebFxo7SbXXozR4ZDnwYzSpqxBq1ezW42dnmkWfJVG87B/Rm9reBESSe9RstoNYydVixPvmbTmR2j76+3Fc4rynpp92uNZqPe3NXLrXDC4cNlMEYMYlY/txZiWs3z1rlpdaxW1+50zrc+yTz5qo8zOabgAqgeYujsV4DLYFDvG9YPTzDH5KKflEW5oaI0lZ7V7/fOt981djwqNMSYE6Mkl3q/VbPMttFv9trtgbH1zPLJCUdh8GVtfjFg9qpvfrJyUhA+ZS+goySdVqN13qs3dloexgji1dqOGlaH0QOKabY6dtvo7QLF9oIg5AQDlbXPrRWVxqDR6XZrW59BdjQq+t4z9lxytCitZZfr+ZxYuKK/8XNrJcW0apZldqw3kfJHMItnX8RHdyQ01HLtOU9yH/PSo0ZpNU2r2akZra1LinN+OcHIGG6UdNOsGYYp/2+XDv+1YGSM7v7659YKyk7PSj4OFJbpw4lyTupdo9Ho261BtyutbH0gUsKEMTHGxDZ8bq2otOS5pWHX3sLKhjOTHlSYgKw8k127KHTs6aKsfm6toOwyBgYURsCyomitxmjU+7V6s9lr9Jrn7ebW6y2bqvtr2Gj6hD3beQxmoRsLHrWnvhez17TMdm3rWeZYXvQdCXsXTASjYepjaZ23rcH2UeNjYbmdfYmGoTtdvc9fDzD50oNGaTRWvV1v2M0f3hd2DDT9R3l6Ca37e/nl6odmUW4zmEwDX349ffkLH8XihxhS2tCu61xwYZ8lLjZ9bi2w2A2zZzYanV1GmD85nsfIGUPMa59bKymDrtHsb71geSwnV0Ew8tzxQ6xn/5+HIitvxZQvzq3W1ltejqWFuTBgURrLLtcuaYJx8fKlKLux+Sl577//HwsEEpJXwggA";

		public override void Up()
			{

Create.Table("Achievement")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("Category")
.AsString(255).Nullable()
.WithColumn("GroupName")
.AsString(255).Nullable()
;


Create.Table("AchievementLevel")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("Level")
.AsInt32().Nullable()
.WithColumn("Requirement")
.AsInt32().Nullable()
.WithColumn("RewardActivityPoints")
.AsInt32().Nullable()
.WithColumn("RewardActivityPointsType")
.AsString(255).Nullable()
.WithColumn("RewardPoints")
.AsInt32().Nullable()
.WithColumn("AchievementRef")
.AsInt32().Nullable()
;


Create.Table("AvatarEffect")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("Activated")
.AsBoolean().Nullable()
.WithColumn("ActivatedAt")
.AsDateTime().Nullable()
.WithColumn("EffectId")
.AsInt32().Nullable()
.WithColumn("TotalDuration")
.AsInt32().Nullable()
.WithColumn("Type")
.AsInt16().Nullable()
.WithColumn("EffectComponentUserEffectComponent_id")
.AsInt32().Nullable()
;


Create.Table("Badge")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("Code")
.AsString(255).Nullable()
.WithColumn("Slot")
.AsInt32().Nullable()
.WithColumn("BadgesUserBadgeComponentRef")
.AsInt32().Nullable()
;


Create.Table("BaseItem")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("discriminator")
.AsString(255).NotNullable()
.WithColumn("AdUrl")
.AsString(255).Nullable()
.WithColumn("AllowInventoryStack")
.AsBoolean().Nullable()
.WithColumn("AllowMarketplaceSell")
.AsBoolean().Nullable()
.WithColumn("AllowRecycle")
.AsBoolean().Nullable()
.WithColumn("AllowTrade")
.AsBoolean().Nullable()
.WithColumn("Classname")
.AsString(255).Nullable()
.WithColumn("DimensionX")
.AsInt32().Nullable()
.WithColumn("DimensionY")
.AsInt32().Nullable()
.WithColumn("FurniLine")
.AsString(255).Nullable()
.WithColumn("Height")
.AsDecimal().Nullable()
.WithColumn("Revision")
.AsInt32().Nullable()
.WithColumn("Stackable")
.AsBoolean().Nullable()
.WithColumn("Description_id")
.AsInt32().Nullable()
.WithColumn("Name_id")
.AsInt32().Nullable()
.WithColumn("DefaultDir")
.AsInt32().Nullable()
.WithColumn("InternalPartColors")
.AsBinary(255).Nullable()
.WithColumn("Color")
.AsInt32().Nullable()
.WithColumn("Song_id")
.AsInt32().Nullable()
.WithColumn("RoomCompetition_id")
.AsInt32().Nullable()
;


Create.Table("CatalogOffer")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("discriminator")
.AsString(255).NotNullable()
.WithColumn("ActivityPointsType")
.AsString(255).Nullable()
.WithColumn("AllowGift")
.AsBoolean().Nullable()
.WithColumn("Badge")
.AsString(255).Nullable()
.WithColumn("ClubLevel")
.AsString(255).Nullable()
.WithColumn("CostActivityPoints")
.AsInt32().Nullable()
.WithColumn("CostCredits")
.AsInt32().Nullable()
.WithColumn("IsRentable")
.AsBoolean().Nullable()
.WithColumn("IsVisible")
.AsBoolean().Nullable()
.WithColumn("Name")
.AsString(255).Nullable()
.WithColumn("Description")
.AsString(255).Nullable()
.WithColumn("ExpiresAt")
.AsDateTime().Nullable()
.WithColumn("Icon")
.AsString(255).Nullable()
.WithColumn("Image")
.AsString(255).Nullable()
.WithColumn("PurchaseLimit")
.AsInt32().Nullable()
.WithColumn("StateCode")
.AsString(255).Nullable()
.WithColumn("CatalogPage_id")
.AsInt32().Nullable()
;


Create.Table("CatalogPage")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("Icon")
.AsInt32().Nullable()
.WithColumn("IsRoot")
.AsBoolean().Nullable()
.WithColumn("MinRank")
.AsInt32().Nullable()
.WithColumn("Type")
.AsInt32().Nullable()
.WithColumn("Visible")
.AsBoolean().Nullable()
.WithColumn("Caption_id")
.AsInt32().Nullable()
.WithColumn("Layout_id")
.AsInt32().Nullable()
.WithColumn("SelectedOffer_id")
.AsInt32().Nullable()
.WithColumn("CatalogPage_id")
.AsInt32().Nullable()
;


Create.Table("CatalogPageLayout")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("discriminator")
.AsString(255).NotNullable()
.WithColumn("HeaderImage")
.AsString(255).Nullable()
.WithColumn("TeaserImage")
.AsString(255).Nullable()
.WithColumn("Content_id")
.AsInt32().Nullable()
.WithColumn("VoucherDescription_id")
.AsInt32().Nullable()
.WithColumn("SpecialImage")
.AsString(255).Nullable()
.WithColumn("TeaserImage1")
.AsString(255).Nullable()
.WithColumn("TeaserImage2")
.AsString(255).Nullable()
.WithColumn("TeaserImage3")
.AsString(255).Nullable()
.WithColumn("HeaderDescription_id")
.AsInt32().Nullable()
.WithColumn("Text_id")
.AsInt32().Nullable()
.WithColumn("Text1_id")
.AsInt32().Nullable()
.WithColumn("Text2_id")
.AsInt32().Nullable()
.WithColumn("Text3_id")
.AsInt32().Nullable()
.WithColumn("Text4_id")
.AsInt32().Nullable()
.WithColumn("Text5_id")
.AsInt32().Nullable()
.WithColumn("Description_id")
.AsInt32().Nullable()
.WithColumn("Enscription_id")
.AsInt32().Nullable()
.WithColumn("SpecialText_id")
.AsInt32().Nullable()
;


Create.Table("CatalogProduct")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("discriminator")
.AsString(255).NotNullable()
.WithColumn("Amount")
.AsInt32().Nullable()
.WithColumn("Item_id")
.AsInt32().Nullable()
.WithColumn("LimitedItemsLeft")
.AsInt32().Nullable()
.WithColumn("LimitedSeriesSize")
.AsInt32().Nullable()
.WithColumn("CatalogOffer_id")
.AsInt32().Nullable()
;


Create.Table("ChatMessage")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("Bubble")
.AsInt32().Nullable()
.WithColumn("Message")
.AsString(255).Nullable()
.WithColumn("Timestamp")
.AsDateTime().Nullable()
.WithColumn("Whisper")
.AsBoolean().Nullable()
.WithColumn("User_id")
.AsInt32().Nullable()
.WithColumn("RoomData_id")
.AsInt32().Nullable()
;


Create.Table("EcotronLevel")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
;


Create.Table("EcotronReward")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("BaseItem_id")
.AsInt32().Nullable()
.WithColumn("EcotronLevel_id")
.AsInt32().Nullable()
;


Create.Table("FriendRequest")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("From_id")
.AsInt32().Nullable()
.WithColumn("To_id")
.AsInt32().Nullable()
.WithColumn("RelationshipsRelationshipComponent_id")
.AsInt32().Nullable()
;


Create.Table("Group")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("AdminOnlyDeco")
.AsInt32().Nullable()
.WithColumn("Badge")
.AsString(255).Nullable()
.WithColumn("CreateTime")
.AsInt32().Nullable()
.WithColumn("Description")
.AsString(255).Nullable()
.WithColumn("Name")
.AsString(255).Nullable()
.WithColumn("State")
.AsInt32().Nullable()
.WithColumn("Colour1_id")
.AsInt32().Nullable()
.WithColumn("Colour2_id")
.AsInt32().Nullable()
.WithColumn("Creator_id")
.AsInt32().Nullable()
.WithColumn("Forum_id")
.AsInt32().Nullable()
.WithColumn("Room_id")
.AsInt32().Nullable()
;


Create.Table("GroupBackGroundColours")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("Colour")
.AsInt32().Nullable()
;


Create.Table("GroupBaseColours")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("Colour")
.AsString(255).Nullable()
;


Create.Table("GroupBases")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("Value1")
.AsString(255).Nullable()
.WithColumn("Value2")
.AsString(255).Nullable()
;


Create.Table("GroupForum")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("ForumDescription")
.AsString(255).Nullable()
.WithColumn("ForumName")
.AsString(255).Nullable()
.WithColumn("ForumScore")
.AsDouble().Nullable()
.WithColumn("WhoCanMod")
.AsInt32().Nullable()
.WithColumn("WhoCanPost")
.AsInt32().Nullable()
.WithColumn("WhoCanRead")
.AsInt32().Nullable()
.WithColumn("WhoCanThread")
.AsInt32().Nullable()
;


Create.Table("GroupForumPost")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("Content")
.AsString(255).Nullable()
.WithColumn("Hidden")
.AsBoolean().Nullable()
.WithColumn("Subject")
.AsString(255).Nullable()
.WithColumn("Timestamp")
.AsDateTime().Nullable()
.WithColumn("HiddenBy_id")
.AsInt32().Nullable()
.WithColumn("Poster_id")
.AsInt32().Nullable()
.WithColumn("GroupForumThread_id")
.AsInt32().Nullable()
;


Create.Table("GroupForumThread")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("CreatedAt")
.AsDateTime().Nullable()
.WithColumn("Hidden")
.AsBoolean().Nullable()
.WithColumn("Locked")
.AsBoolean().Nullable()
.WithColumn("Pinned")
.AsBoolean().Nullable()
.WithColumn("Subject")
.AsString(255).Nullable()
.WithColumn("Creator_id")
.AsInt32().Nullable()
.WithColumn("HiddenBy_id")
.AsInt32().Nullable()
.WithColumn("GroupForum_id")
.AsInt32().Nullable()
;


Create.Table("GroupSymbolColours")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("Colour")
.AsInt32().Nullable()
;


Create.Table("GroupSymbols")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("Value1")
.AsString(255).Nullable()
.WithColumn("Value2")
.AsString(255).Nullable()
;


Create.Table("HallOfFameElement")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("Competition")
.AsString(255).Nullable()
.WithColumn("Score")
.AsInt32().Nullable()
.WithColumn("User_id")
.AsInt32().Nullable()
;


Create.Table("HotelLandingManager")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("FurniReward_id")
.AsInt32().Nullable()
;


Create.Table("HotelLandingPromos")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("Body")
.AsString(255).Nullable()
.WithColumn("Button")
.AsString(255).Nullable()
.WithColumn("CreatedAt")
.AsDateTime().Nullable()
.WithColumn("Image")
.AsString(255).Nullable()
.WithColumn("Title")
.AsString(255).Nullable()
.WithColumn("LinkUrl")
.AsString(255).Nullable()
.WithColumn("HotelLandingManager_id")
.AsInt32().Nullable()
;


Create.Table("Link")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("IsInternal")
.AsBoolean().Nullable()
.WithColumn("Text")
.AsString(255).Nullable()
.WithColumn("URL")
.AsString(255).Nullable()
.WithColumn("ChatMessage_id")
.AsInt32().Nullable()
;


Create.Table("MessengerMessage")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("Read")
.AsBoolean().Nullable()
.WithColumn("Text")
.AsString(255).Nullable()
.WithColumn("Timestamp")
.AsDateTime().Nullable()
.WithColumn("UnfilteredText")
.AsString(255).Nullable()
.WithColumn("From_id")
.AsInt32().Nullable()
.WithColumn("To_id")
.AsInt32().Nullable()
;


Create.Table("Minimail")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("Seen")
.AsBoolean().Nullable()
.WithColumn("UserInfo_id")
.AsInt32().Nullable()
;


Create.Table("ModerationTemplate")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("AvatarBan")
.AsBoolean().Nullable()
.WithColumn("BanHours")
.AsInt16().Nullable()
.WithColumn("BanMessage")
.AsString(255).Nullable()
.WithColumn("Caption")
.AsString(255).Nullable()
.WithColumn("Category")
.AsInt16().Nullable()
.WithColumn("CName")
.AsString(255).Nullable()
.WithColumn("Mute")
.AsBoolean().Nullable()
.WithColumn("TradeLock")
.AsBoolean().Nullable()
.WithColumn("WarningMessage")
.AsString(255).Nullable()
;


Create.Table("MoodlightData")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("Enabled")
.AsBoolean().Nullable()
.WithColumn("CurrentPreset_id")
.AsInt32().Nullable()
;


Create.Table("MoodlightPreset")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("BackgroundOnly")
.AsBoolean().Nullable()
.WithColumn("ColorCode")
.AsString(255).Nullable()
.WithColumn("ColorIntensity")
.AsInt32().Nullable()
.WithColumn("MoodlightData_id")
.AsInt32().Nullable()
;


Create.Table("NavigatorCategory")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("discriminator")
.AsString(255).NotNullable()
.WithColumn("Caption")
.AsString(255).Nullable()
.WithColumn("IsImage")
.AsBoolean().Nullable()
.WithColumn("IsOpened")
.AsBoolean().Nullable()
.WithColumn("MinRank")
.AsInt32().Nullable()
.WithColumn("Visible")
.AsBoolean().Nullable()
.WithColumn("NavigatorCategoryRef")
.AsInt32().Nullable()
;


Create.Table("Poll")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("Invitation")
.AsString(255).Nullable()
.WithColumn("PollName")
.AsString(255).Nullable()
.WithColumn("Prize")
.AsString(255).Nullable()
.WithColumn("Thanks")
.AsString(255).Nullable()
.WithColumn("Room_id")
.AsInt32().Nullable()
;


Create.Table("PollQuestion")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("AnswerType")
.AsString(255).Nullable()
.WithColumn("CorrectAnswer")
.AsString(255).Nullable()
.WithColumn("Question")
.AsString(255).Nullable()
.WithColumn("Poll_id")
.AsInt32().Nullable()
;


Create.Table("Relationship")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("Deleted")
.AsBoolean().Nullable()
.WithColumn("Type")
.AsInt32().Nullable()
.WithColumn("Friend_id")
.AsInt32().Nullable()
.WithColumn("RelationshipsRelationshipComponentRef")
.AsInt32().Nullable()
;


Create.Table("RoomCompetition")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("Name")
.AsString(255).Nullable()
;


Create.Table("RoomCompetitionEntry")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("Votes")
.AsInt32().Nullable()
.WithColumn("Room_id")
.AsInt32().Nullable()
.WithColumn("RoomCompetition_id")
.AsInt32().Nullable()
;


Create.Table("RoomData")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("AllowPets")
.AsBoolean().Nullable()
.WithColumn("AllowPetsEating")
.AsBoolean().Nullable()
.WithColumn("AllowRightsOverride")
.AsBoolean().Nullable()
.WithColumn("AllowWalkThrough")
.AsBoolean().Nullable()
.WithColumn("CCTs")
.AsString(255).Nullable()
.WithColumn("Description")
.AsString(255).Nullable()
.WithColumn("Floor")
.AsDecimal().Nullable()
.WithColumn("FloorThickness")
.AsInt32().Nullable()
.WithColumn("HideWall")
.AsBoolean().Nullable()
.WithColumn("IsMuted")
.AsBoolean().Nullable()
.WithColumn("LandScape")
.AsDecimal().Nullable()
.WithColumn("Model")
.AsInt32().Nullable()
.WithColumn("Name")
.AsString(255).Nullable()
.WithColumn("NavigatorImage")
.AsString(255).Nullable()
.WithColumn("Password")
.AsString(255).Nullable()
.WithColumn("Score")
.AsInt32().Nullable()
.WithColumn("State")
.AsInt32().Nullable()
.WithColumn("TradeState")
.AsInt32().Nullable()
.WithColumn("Type")
.AsString(255).Nullable()
.WithColumn("UsersMax")
.AsInt32().Nullable()
.WithColumn("WallHeight")
.AsInt32().Nullable()
.WithColumn("WallPaper")
.AsDecimal().Nullable()
.WithColumn("WallThickness")
.AsInt32().Nullable()
.WithColumn("Category_id")
.AsInt32().Nullable()
.WithColumn("Event_id")
.AsInt32().Nullable()
.WithColumn("Group_id")
.AsInt32().Nullable()
.WithColumn("Owner_id")
.AsInt32().Nullable()
.WithColumn("ChatBalloon")
.AsInt32().Nullable()
.WithColumn("ChatFloodProtection")
.AsInt32().Nullable()
.WithColumn("ChatMaxDistance")
.AsInt32().Nullable()
.WithColumn("ChatSpeed")
.AsInt32().Nullable()
.WithColumn("ChatType")
.AsInt32().Nullable()
.WithColumn("ModerationSettingsWhoCanBan")
.AsInt32().Nullable()
.WithColumn("ModerationSettingsWhoCanKick")
.AsInt32().Nullable()
.WithColumn("ModerationSettingsWhoCanMute")
.AsInt32().Nullable()
;


Create.Table("RoomEvent")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("Category")
.AsInt32().Nullable()
.WithColumn("Description")
.AsString(255).Nullable()
.WithColumn("ExpiresAt")
.AsDateTime().Nullable()
.WithColumn("Name")
.AsString(255).Nullable()
;


Create.Table("RoomMute")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("ExpiresAt")
.AsDateTime().Nullable()
.WithColumn("Entity_id")
.AsInt32().Nullable()
.WithColumn("RoomData_id")
.AsInt32().Nullable()
;


Create.Table("SongData")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("Artist")
.AsString(255).Nullable()
.WithColumn("CodeName")
.AsString(255).Nullable()
.WithColumn("Data")
.AsString(255).Nullable()
.WithColumn("LengthMiliseconds")
.AsInt32().Nullable()
.WithColumn("Name")
.AsString(255).Nullable()
;


Create.Table("Subscription")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("ActivateTime")
.AsDateTime().Nullable()
.WithColumn("ExpireTime")
.AsDateTime().Nullable()
;


Create.Table("SupportTicket")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("Category")
.AsInt32().Nullable()
.WithColumn("CloseReason")
.AsInt32().Nullable()
.WithColumn("CreatedAt")
.AsDateTime().Nullable()
.WithColumn("Message")
.AsString(255).Nullable()
.WithColumn("Score")
.AsInt32().Nullable()
.WithColumn("Status")
.AsString(255).Nullable()
.WithColumn("Type")
.AsInt32().Nullable()
.WithColumn("ReportedUser_id")
.AsInt32().Nullable()
.WithColumn("Room_id")
.AsInt32().Nullable()
.WithColumn("Sender_id")
.AsInt32().Nullable()
.WithColumn("Staff_id")
.AsInt32().Nullable()
;


Create.Table("Talent")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("Type")
.AsInt32().Nullable()
.WithColumn("PrizeItem_id")
.AsInt32().Nullable()
;


Create.Table("TalentLevel")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("Level")
.AsInt32().Nullable()
.WithColumn("Achievement_id")
.AsInt32().Nullable()
.WithColumn("TalentRef")
.AsInt32().Nullable()
;


Create.Table("TradeLock")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("ExpiresAt")
.AsDateTime().Nullable()
.WithColumn("UserInfo_id")
.AsInt32().Nullable()
;


Create.Table("Translation")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("LanguageId")
.AsInt32().NotNullable()
.WithColumn("Value")
.AsString(255).NotNullable()
.WithColumn("TString_id")
.AsInt32().Nullable()
;


Create.Table("TString")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("Value")
.AsString(255).NotNullable()
;


Create.Table("UserAchievement")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("Progress")
.AsInt32().Nullable()
.WithColumn("Achievement_id")
.AsInt32().Nullable()
.WithColumn("Level_id")
.AsInt32().Nullable()
.WithColumn("UserInfo_id")
.AsInt32().Nullable()
;


Create.Table("UserBan")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("ExpiresAt")
.AsDateTime().Nullable()
.WithColumn("IP")
.AsString(255).Nullable()
.WithColumn("MachineId")
.AsString(255).Nullable()
.WithColumn("Reason")
.AsString(255).Nullable()
.WithColumn("User_id")
.AsInt32().Nullable()
;


Create.Table("UserCaution")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("UserInfo_id")
.AsInt32().Nullable()
;


Create.Table("UserItem")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
;


Create.Table("UserSearchLog")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("Value1")
.AsString(255).Nullable()
.WithColumn("Value2")
.AsString(255).Nullable()
.WithColumn("UserInfo_id")
.AsInt32().Nullable()
;


Create.Table("UserTalent")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("State")
.AsInt32().Nullable()
.WithColumn("Level_id")
.AsInt32().Nullable()
.WithColumn("Talent_id")
.AsInt32().Nullable()
.WithColumn("UserInfoRef")
.AsInt32().Nullable()
;


Create.Table("WardrobeItem")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("Gender")
.AsString(255).Nullable()
.WithColumn("Look")
.AsString(255).Nullable()
.WithColumn("Slot")
.AsInt32().Nullable()
.WithColumn("InventoryInventoryRef")
.AsInt32().Nullable()
;


Create.Table("YoutubeVideo")

.WithColumn("Id")
.AsString(255).NotNullable().PrimaryKey()
.WithColumn("Description")
.AsString(255).Nullable()
.WithColumn("Name")
.AsString(255).Nullable()
;


Create.Table("BotInfo")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("Gender")
.AsFixedLengthString(255).Nullable()
.WithColumn("Look")
.AsString(255).Nullable()
.WithColumn("Motto")
.AsString(255).Nullable()
.WithColumn("Name")
.AsString(255).NotNullable()
.WithColumn("Owner_id")
.AsInt32().Nullable()
;


Create.Table("FloorItem")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("discriminator")
.AsString(255).NotNullable()
.WithColumn("Owner_id")
.AsInt32().Nullable()
.WithColumn("Badge")
.AsString(255).Nullable()
.WithColumn("CreatedAt")
.AsDateTime().Nullable()
.WithColumn("BaseItem_id")
.AsInt32().Nullable()
.WithColumn("LookFemale")
.AsString(255).Nullable()
.WithColumn("LookMale")
.AsString(255).Nullable()
.WithColumn("Gender")
.AsString(255).Nullable()
.WithColumn("Look")
.AsString(255).Nullable()
.WithColumn("Race")
.AsInt32().Nullable()
.WithColumn("Message")
.AsString(255).Nullable()
.WithColumn("User_id")
.AsInt32().Nullable()
.WithColumn("PlayingVideo_id")
.AsString(255).Nullable()
.WithColumn("InventoryInventory_id")
.AsInt32().Nullable()
;


Create.Table("PetInfo")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("AnyoneCanRide")
.AsBoolean().Nullable()
.WithColumn("BreadingTile_X")
.AsFloat().Nullable()
.WithColumn("BreadingTile_Y")
.AsFloat().Nullable()
.WithColumn("BreadingTile_Z")
.AsFloat().Nullable()
.WithColumn("Color")
.AsString(255).Nullable()
.WithColumn("CreatedAt")
.AsDateTime().Nullable()
.WithColumn("Energy")
.AsInt32().Nullable()
.WithColumn("Experience")
.AsInt32().Nullable()
.WithColumn("Hair")
.AsInt32().Nullable()
.WithColumn("HairDye")
.AsInt32().Nullable()
.WithColumn("HaveSaddle")
.AsBoolean().Nullable()
.WithColumn("LastHealth")
.AsDateTime().Nullable()
.WithColumn("Nutrition")
.AsInt32().Nullable()
.WithColumn("PlacedInRoom")
.AsBoolean().Nullable()
.WithColumn("Position_X")
.AsFloat().Nullable()
.WithColumn("Position_Y")
.AsFloat().Nullable()
.WithColumn("Position_Z")
.AsFloat().Nullable()
.WithColumn("Race")
.AsInt32().Nullable()
.WithColumn("RaceId")
.AsInt32().Nullable()
.WithColumn("Rarity")
.AsInt32().Nullable()
.WithColumn("Respect")
.AsInt32().Nullable()
.WithColumn("Type")
.AsString(255).Nullable()
.WithColumn("WaitingForBreading")
.AsInt32().Nullable()
.WithColumn("Motto")
.AsString(255).Nullable()
.WithColumn("Name")
.AsString(255).NotNullable()
.WithColumn("Owner_id")
.AsInt32().Nullable()
.WithColumn("Room_id")
.AsInt32().Nullable()
;


Create.Table("PetItem")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("BaseItem_id")
.AsInt32().Nullable()
.WithColumn("Info_id")
.AsInt32().Nullable()
.WithColumn("Owner_id")
.AsInt32().Nullable()
.WithColumn("InventoryInventory_id")
.AsInt32().Nullable()
;


Create.Table("UserInfo")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("AppearOffline")
.AsBoolean().Nullable()
.WithColumn("BobbaFiltered")
.AsInt32().Nullable()
.WithColumn("CreateDate")
.AsDateTime().Nullable()
.WithColumn("Email")
.AsString(255).Nullable()
.WithColumn("Gender")
.AsString(255).Nullable()
.WithColumn("HasFriendRequestsDisabled")
.AsBoolean().Nullable()
.WithColumn("HideInRoom")
.AsBoolean().Nullable()
.WithColumn("LastIp")
.AsString(255).Nullable()
.WithColumn("LastOnline")
.AsDateTime().Nullable()
.WithColumn("Look")
.AsString(255).Nullable()
.WithColumn("Muted")
.AsBoolean().Nullable()
.WithColumn("Rank")
.AsInt32().Nullable()
.WithColumn("SpamFloodTime")
.AsDateTime().Nullable()
.WithColumn("SpectatorMode")
.AsBoolean().Nullable()
.WithColumn("Motto")
.AsString(255).Nullable()
.WithColumn("Name")
.AsString(255).NotNullable()
.WithColumn("FavouriteGroup_id")
.AsInt32().Nullable()
.WithColumn("HomeRoom_id")
.AsInt32().Nullable()
.WithColumn("Subscription_id")
.AsInt32().Nullable()
.WithColumn("BuilderInfoBuildersExpire")
.AsInt32().Nullable()
.WithColumn("BuilderInfoBuildersItemsMax")
.AsInt32().Nullable()
.WithColumn("BuilderInfoBuildersItemsUsed")
.AsInt32().Nullable()
.WithColumn("EffectComponentActiveEffect_id")
.AsInt32().Nullable()
.WithColumn("PreferencesChatBubbleStyle")
.AsInt32().Nullable()
.WithColumn("PreferencesDisableCameraFollow")
.AsBoolean().Nullable()
.WithColumn("PreferencesIgnoreRoomInvite")
.AsBoolean().Nullable()
.WithColumn("PreferencesNavigatorHeight")
.AsInt32().Nullable()
.WithColumn("PreferencesNavigatorWidth")
.AsInt32().Nullable()
.WithColumn("PreferencesNewnaviX")
.AsInt32().Nullable()
.WithColumn("PreferencesNewnaviY")
.AsInt32().Nullable()
.WithColumn("PreferencesPreferOldChat")
.AsBoolean().Nullable()
.WithColumn("PreferencesVolume1")
.AsInt32().Nullable()
.WithColumn("PreferencesVolume2")
.AsInt32().Nullable()
.WithColumn("PreferencesVolume3")
.AsInt32().Nullable()
.WithColumn("RespectDailyCompetitionVotes")
.AsInt32().Nullable()
.WithColumn("RespectDailyPetRespectPoints")
.AsInt32().Nullable()
.WithColumn("RespectDailyRespectPoints")
.AsInt32().Nullable()
.WithColumn("RespectRespect")
.AsInt32().Nullable()
.WithColumn("WalletAchievementPoints")
.AsInt32().Nullable()
.WithColumn("WalletCredits")
.AsInt32().Nullable()
.WithColumn("UserInfo_id")
.AsInt32().Nullable()
;


Create.Table("WallItem")

.WithColumn("Id")
.AsInt32().NotNullable().Identity().PrimaryKey()
.WithColumn("discriminator")
.AsString(255).NotNullable()
.WithColumn("Owner_id")
.AsInt32().Nullable()
.WithColumn("Data_id")
.AsInt32().Nullable()
.WithColumn("BaseItem_id")
.AsInt32().Nullable()
.WithColumn("Color")
.AsString(255).Nullable()
.WithColumn("Text")
.AsString(255).Nullable()
.WithColumn("Number")
.AsDouble().Nullable()
.WithColumn("InventoryInventory_id")
.AsInt32().Nullable()
;


Create.ForeignKey("FK8AC9B5B9B51F9CE4")
      .FromTable("AchievementLevel")
	  .ForeignColumns("AchievementRef")
	  .ToTable("Achievement")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK1E173FAE75ECFEED")
      .FromTable("AvatarEffect")
	  .ForeignColumns("EffectComponentUserEffectComponent_id")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK50A53C35E35C297E")
      .FromTable("Badge")
	  .ForeignColumns("BadgesUserBadgeComponentRef")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK5B7D82102AB945AC")
      .FromTable("BaseItem")
	  .ForeignColumns("Description_id")
	  .ToTable("TString")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK5B7D8210C92BB699")
      .FromTable("BaseItem")
	  .ForeignColumns("Name_id")
	  .ToTable("TString")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK5B7D82104E121F3F")
      .FromTable("BaseItem")
	  .ForeignColumns("Song_id")
	  .ToTable("SongData")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK5B7D8210EA5B8F6B")
      .FromTable("BaseItem")
	  .ForeignColumns("RoomCompetition_id")
	  .ToTable("RoomCompetition")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK7C1765AF218EC187")
      .FromTable("CatalogOffer")
	  .ForeignColumns("CatalogPage_id")
	  .ToTable("CatalogPage")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKBE955E6ED94EBF50")
      .FromTable("CatalogPage")
	  .ForeignColumns("Caption_id")
	  .ToTable("TString")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKBE955E6E91DBDC5F")
      .FromTable("CatalogPage")
	  .ForeignColumns("Layout_id")
	  .ToTable("CatalogPageLayout")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKBE955E6E8C4FC83D")
      .FromTable("CatalogPage")
	  .ForeignColumns("SelectedOffer_id")
	  .ToTable("CatalogOffer")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKBE955E6E218EC187")
      .FromTable("CatalogPage")
	  .ForeignColumns("CatalogPage_id")
	  .ToTable("CatalogPage")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKCFA36A78DFF0BB9B")
      .FromTable("CatalogPageLayout")
	  .ForeignColumns("Content_id")
	  .ToTable("TString")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKCFA36A785F2BA7BA")
      .FromTable("CatalogPageLayout")
	  .ForeignColumns("VoucherDescription_id")
	  .ToTable("TString")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKCFA36A7824B24313")
      .FromTable("CatalogPageLayout")
	  .ForeignColumns("HeaderDescription_id")
	  .ToTable("TString")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKCFA36A784D9405FF")
      .FromTable("CatalogPageLayout")
	  .ForeignColumns("Text_id")
	  .ToTable("TString")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKCFA36A784C7DC080")
      .FromTable("CatalogPageLayout")
	  .ForeignColumns("Text1_id")
	  .ToTable("TString")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKCFA36A784C7DC0E1")
      .FromTable("CatalogPageLayout")
	  .ForeignColumns("Text2_id")
	  .ToTable("TString")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKCFA36A784C7DC0C2")
      .FromTable("CatalogPageLayout")
	  .ForeignColumns("Text3_id")
	  .ToTable("TString")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKCFA36A784C7DC123")
      .FromTable("CatalogPageLayout")
	  .ForeignColumns("Text4_id")
	  .ToTable("TString")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKCFA36A784C7DC1FC")
      .FromTable("CatalogPageLayout")
	  .ForeignColumns("Text5_id")
	  .ToTable("TString")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKCFA36A782AB945AC")
      .FromTable("CatalogPageLayout")
	  .ForeignColumns("Description_id")
	  .ToTable("TString")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKCFA36A78F5E88CB8")
      .FromTable("CatalogPageLayout")
	  .ForeignColumns("Enscription_id")
	  .ToTable("TString")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKCFA36A787FC1D4B2")
      .FromTable("CatalogPageLayout")
	  .ForeignColumns("SpecialText_id")
	  .ToTable("TString")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK55125810F8107B18")
      .FromTable("CatalogProduct")
	  .ForeignColumns("Item_id")
	  .ToTable("BaseItem")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK551258103DF7F84B")
      .FromTable("CatalogProduct")
	  .ForeignColumns("CatalogOffer_id")
	  .ToTable("CatalogOffer")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKB9820A059DFFE519")
      .FromTable("ChatMessage")
	  .ForeignColumns("User_id")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKB9820A0575DE907")
      .FromTable("ChatMessage")
	  .ForeignColumns("RoomData_id")
	  .ToTable("RoomData")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK89B740A5F3865CDB")
      .FromTable("EcotronReward")
	  .ForeignColumns("BaseItem_id")
	  .ToTable("BaseItem")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK89B740A5321F574B")
      .FromTable("EcotronReward")
	  .ForeignColumns("EcotronLevel_id")
	  .ToTable("EcotronLevel")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK2357C3FFAB03AA0")
      .FromTable("FriendRequest")
	  .ForeignColumns("From_id")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK2357C3FB550214F")
      .FromTable("FriendRequest")
	  .ForeignColumns("To_id")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK2357C3F42A90AD8")
      .FromTable("FriendRequest")
	  .ForeignColumns("RelationshipsRelationshipComponent_id")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKFBBA8E2754C3043A")
      .FromTable("Group")
	  .ForeignColumns("Colour1_id")
	  .ToTable("GroupSymbolColours")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKFBBA8E273F6DBC9")
      .FromTable("Group")
	  .ForeignColumns("Colour2_id")
	  .ToTable("GroupBackGroundColours")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKFBBA8E275D5B1940")
      .FromTable("Group")
	  .ForeignColumns("Creator_id")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKFBBA8E27F428CA4C")
      .FromTable("Group")
	  .ForeignColumns("Forum_id")
	  .ToTable("GroupForum")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKFBBA8E27F8FE1E37")
      .FromTable("Group")
	  .ForeignColumns("Room_id")
	  .ToTable("RoomData")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK7A678264B2E044D5")
      .FromTable("GroupForumPost")
	  .ForeignColumns("HiddenBy_id")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK7A678264ECC84987")
      .FromTable("GroupForumPost")
	  .ForeignColumns("Poster_id")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK7A67826426F99CBB")
      .FromTable("GroupForumPost")
	  .ForeignColumns("GroupForumThread_id")
	  .ToTable("GroupForumThread")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK3466FFA5D5B1940")
      .FromTable("GroupForumThread")
	  .ForeignColumns("Creator_id")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK3466FFAB2E044D5")
      .FromTable("GroupForumThread")
	  .ForeignColumns("HiddenBy_id")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK3466FFA86438333")
      .FromTable("GroupForumThread")
	  .ForeignColumns("GroupForum_id")
	  .ToTable("GroupForum")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKA7354D6D9DFFE519")
      .FromTable("HallOfFameElement")
	  .ForeignColumns("User_id")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKDF85EFA62FE6A4E8")
      .FromTable("HotelLandingManager")
	  .ForeignColumns("FurniReward_id")
	  .ToTable("BaseItem")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKDE8AA2F94C9CB897")
      .FromTable("HotelLandingPromos")
	  .ForeignColumns("HotelLandingManager_id")
	  .ToTable("HotelLandingManager")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK13647ACAD00CE56F")
      .FromTable("Link")
	  .ForeignColumns("ChatMessage_id")
	  .ToTable("ChatMessage")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKDB8AFE64FAB03AA0")
      .FromTable("MessengerMessage")
	  .ForeignColumns("From_id")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKDB8AFE64B550214F")
      .FromTable("MessengerMessage")
	  .ForeignColumns("To_id")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK5DE3454A7ED30A0B")
      .FromTable("Minimail")
	  .ForeignColumns("UserInfo_id")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK1C0D29333C014DEF")
      .FromTable("MoodlightData")
	  .ForeignColumns("CurrentPreset_id")
	  .ToTable("MoodlightPreset")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK4E12B0E811B558E3")
      .FromTable("MoodlightPreset")
	  .ForeignColumns("MoodlightData_id")
	  .ToTable("MoodlightData")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKA1A08B333E8646A")
      .FromTable("NavigatorCategory")
	  .ForeignColumns("NavigatorCategoryRef")
	  .ToTable("NavigatorCategory")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKC2ECA37F8FE1E37")
      .FromTable("Poll")
	  .ForeignColumns("Room_id")
	  .ToTable("RoomData")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK55F7879950AE97E7")
      .FromTable("PollQuestion")
	  .ForeignColumns("Poll_id")
	  .ToTable("Poll")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKE2E4B70238A212C")
      .FromTable("Relationship")
	  .ForeignColumns("Friend_id")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKE2E4B70D94B326B")
      .FromTable("Relationship")
	  .ForeignColumns("RelationshipsRelationshipComponentRef")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK870DFD1CF8FE1E37")
      .FromTable("RoomCompetitionEntry")
	  .ForeignColumns("Room_id")
	  .ToTable("RoomData")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK870DFD1CEA5B8F6B")
      .FromTable("RoomCompetitionEntry")
	  .ForeignColumns("RoomCompetition_id")
	  .ToTable("RoomCompetition")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK2F2D6D4BE9D7EC2C")
      .FromTable("RoomData")
	  .ForeignColumns("Category_id")
	  .ToTable("NavigatorCategory")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK2F2D6D4BA1F9B824")
      .FromTable("RoomData")
	  .ForeignColumns("Event_id")
	  .ToTable("RoomEvent")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK2F2D6D4BE2440413")
      .FromTable("RoomData")
	  .ForeignColumns("Group_id")
	  .ToTable("Group")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK2F2D6D4B8005458D")
      .FromTable("RoomData")
	  .ForeignColumns("Owner_id")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK9873AF411868503")
      .FromTable("RoomMute")
	  .ForeignColumns("Entity_id")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK9873AF475DE907")
      .FromTable("RoomMute")
	  .ForeignColumns("RoomData_id")
	  .ToTable("RoomData")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKEB3E86EB851EAF78")
      .FromTable("SupportTicket")
	  .ForeignColumns("ReportedUser_id")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKEB3E86EBF8FE1E37")
      .FromTable("SupportTicket")
	  .ForeignColumns("Room_id")
	  .ToTable("RoomData")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKEB3E86EB772B2EB7")
      .FromTable("SupportTicket")
	  .ForeignColumns("Sender_id")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKEB3E86EBCA13F3E2")
      .FromTable("SupportTicket")
	  .ForeignColumns("Staff_id")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK83599A5C261A9BE8")
      .FromTable("Talent")
	  .ForeignColumns("PrizeItem_id")
	  .ToTable("BaseItem")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKBA01A84E810D3643")
      .FromTable("TalentLevel")
	  .ForeignColumns("Achievement_id")
	  .ToTable("Achievement")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKBA01A84E4DB85828")
      .FromTable("TalentLevel")
	  .ForeignColumns("TalentRef")
	  .ToTable("Talent")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK255931D7ED30A0B")
      .FromTable("TradeLock")
	  .ForeignColumns("UserInfo_id")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK7140856B5DC04EAB")
      .FromTable("Translation")
	  .ForeignColumns("TString_id")
	  .ToTable("TString")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK803E7074810D3643")
      .FromTable("UserAchievement")
	  .ForeignColumns("Achievement_id")
	  .ToTable("Achievement")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK803E70741F15AF0")
      .FromTable("UserAchievement")
	  .ForeignColumns("Level_id")
	  .ToTable("AchievementLevel")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK803E70747ED30A0B")
      .FromTable("UserAchievement")
	  .ForeignColumns("UserInfo_id")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKED490F329DFFE519")
      .FromTable("UserBan")
	  .ForeignColumns("User_id")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK924B42BA7ED30A0B")
      .FromTable("UserCaution")
	  .ForeignColumns("UserInfo_id")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK5A0232137ED30A0B")
      .FromTable("UserSearchLog")
	  .ForeignColumns("UserInfo_id")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK43646CE7E48F7765")
      .FromTable("UserTalent")
	  .ForeignColumns("Level_id")
	  .ToTable("TalentLevel")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK43646CE7EDD1ABEB")
      .FromTable("UserTalent")
	  .ForeignColumns("Talent_id")
	  .ToTable("Talent")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK43646CE79EAA9678")
      .FromTable("UserTalent")
	  .ForeignColumns("UserInfoRef")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK1A40EC5BA3955DBB")
      .FromTable("WardrobeItem")
	  .ForeignColumns("InventoryInventoryRef")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK4246912B8005458D")
      .FromTable("BotInfo")
	  .ForeignColumns("Owner_id")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKCC47F5B98005458D")
      .FromTable("FloorItem")
	  .ForeignColumns("Owner_id")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKCC47F5B92D27880A")
      .FromTable("FloorItem")
	  .ForeignColumns("BaseItem_id")
	  .ToTable("BaseItem")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKCC47F5B99DFFE519")
      .FromTable("FloorItem")
	  .ForeignColumns("User_id")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKCC47F5B9CE0EEC7E")
      .FromTable("FloorItem")
	  .ForeignColumns("PlayingVideo_id")
	  .ToTable("YoutubeVideo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKCC47F5B93E370B44")
      .FromTable("FloorItem")
	  .ForeignColumns("InventoryInventory_id")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK30BBCBCB8005458D")
      .FromTable("PetInfo")
	  .ForeignColumns("Owner_id")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK30BBCBCBF8FE1E37")
      .FromTable("PetInfo")
	  .ForeignColumns("Room_id")
	  .ToTable("RoomData")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK18B22AF4D88BF8C")
      .FromTable("PetItem")
	  .ForeignColumns("BaseItem_id")
	  .ToTable("BaseItem")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK18B22AF4430B2F0C")
      .FromTable("PetItem")
	  .ForeignColumns("Info_id")
	  .ToTable("PetInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK18B22AF48005458D")
      .FromTable("PetItem")
	  .ForeignColumns("Owner_id")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FK18B22AF43E370B44")
      .FromTable("PetItem")
	  .ForeignColumns("InventoryInventory_id")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKB21A01339293653E")
      .FromTable("UserInfo")
	  .ForeignColumns("FavouriteGroup_id")
	  .ToTable("Group")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKB21A0133C93EC504")
      .FromTable("UserInfo")
	  .ForeignColumns("HomeRoom_id")
	  .ToTable("RoomData")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKB21A0133C465ED37")
      .FromTable("UserInfo")
	  .ForeignColumns("Subscription_id")
	  .ToTable("Subscription")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKB21A0133E1512F38")
      .FromTable("UserInfo")
	  .ForeignColumns("EffectComponentActiveEffect_id")
	  .ToTable("AvatarEffect")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKB21A01337ED30A0B")
      .FromTable("UserInfo")
	  .ForeignColumns("UserInfo_id")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKF2C9C2278005458D")
      .FromTable("WallItem")
	  .ForeignColumns("Owner_id")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKF2C9C227D8B3AC")
      .FromTable("WallItem")
	  .ForeignColumns("Data_id")
	  .ToTable("MoodlightData")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKF2C9C227CF2C6E49")
      .FromTable("WallItem")
	  .ForeignColumns("BaseItem_id")
	  .ToTable("BaseItem")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);


Create.ForeignKey("FKF2C9C2273E370B44")
      .FromTable("WallItem")
	  .ForeignColumns("InventoryInventory_id")
	  .ToTable("UserInfo")
	  .PrimaryColumns("Id")
	  .OnDelete(Rule.None)
	  .OnUpdate(Rule.None);

			}
		public override void Down()
			{

Delete.ForeignKey("FKF2C9C2273E370B44")
      .OnTable("WallItem");


Delete.ForeignKey("FKF2C9C227CF2C6E49")
      .OnTable("WallItem");


Delete.ForeignKey("FKF2C9C227D8B3AC")
      .OnTable("WallItem");


Delete.ForeignKey("FKF2C9C2278005458D")
      .OnTable("WallItem");


Delete.ForeignKey("FKB21A01337ED30A0B")
      .OnTable("UserInfo");


Delete.ForeignKey("FKB21A0133E1512F38")
      .OnTable("UserInfo");


Delete.ForeignKey("FKB21A0133C465ED37")
      .OnTable("UserInfo");


Delete.ForeignKey("FKB21A0133C93EC504")
      .OnTable("UserInfo");


Delete.ForeignKey("FKB21A01339293653E")
      .OnTable("UserInfo");


Delete.ForeignKey("FK18B22AF43E370B44")
      .OnTable("PetItem");


Delete.ForeignKey("FK18B22AF48005458D")
      .OnTable("PetItem");


Delete.ForeignKey("FK18B22AF4430B2F0C")
      .OnTable("PetItem");


Delete.ForeignKey("FK18B22AF4D88BF8C")
      .OnTable("PetItem");


Delete.ForeignKey("FK30BBCBCBF8FE1E37")
      .OnTable("PetInfo");


Delete.ForeignKey("FK30BBCBCB8005458D")
      .OnTable("PetInfo");


Delete.ForeignKey("FKCC47F5B93E370B44")
      .OnTable("FloorItem");


Delete.ForeignKey("FKCC47F5B9CE0EEC7E")
      .OnTable("FloorItem");


Delete.ForeignKey("FKCC47F5B99DFFE519")
      .OnTable("FloorItem");


Delete.ForeignKey("FKCC47F5B92D27880A")
      .OnTable("FloorItem");


Delete.ForeignKey("FKCC47F5B98005458D")
      .OnTable("FloorItem");


Delete.ForeignKey("FK4246912B8005458D")
      .OnTable("BotInfo");


Delete.ForeignKey("FK1A40EC5BA3955DBB")
      .OnTable("WardrobeItem");


Delete.ForeignKey("FK43646CE79EAA9678")
      .OnTable("UserTalent");


Delete.ForeignKey("FK43646CE7EDD1ABEB")
      .OnTable("UserTalent");


Delete.ForeignKey("FK43646CE7E48F7765")
      .OnTable("UserTalent");


Delete.ForeignKey("FK5A0232137ED30A0B")
      .OnTable("UserSearchLog");


Delete.ForeignKey("FK924B42BA7ED30A0B")
      .OnTable("UserCaution");


Delete.ForeignKey("FKED490F329DFFE519")
      .OnTable("UserBan");


Delete.ForeignKey("FK803E70747ED30A0B")
      .OnTable("UserAchievement");


Delete.ForeignKey("FK803E70741F15AF0")
      .OnTable("UserAchievement");


Delete.ForeignKey("FK803E7074810D3643")
      .OnTable("UserAchievement");


Delete.ForeignKey("FK7140856B5DC04EAB")
      .OnTable("Translation");


Delete.ForeignKey("FK255931D7ED30A0B")
      .OnTable("TradeLock");


Delete.ForeignKey("FKBA01A84E4DB85828")
      .OnTable("TalentLevel");


Delete.ForeignKey("FKBA01A84E810D3643")
      .OnTable("TalentLevel");


Delete.ForeignKey("FK83599A5C261A9BE8")
      .OnTable("Talent");


Delete.ForeignKey("FKEB3E86EBCA13F3E2")
      .OnTable("SupportTicket");


Delete.ForeignKey("FKEB3E86EB772B2EB7")
      .OnTable("SupportTicket");


Delete.ForeignKey("FKEB3E86EBF8FE1E37")
      .OnTable("SupportTicket");


Delete.ForeignKey("FKEB3E86EB851EAF78")
      .OnTable("SupportTicket");


Delete.ForeignKey("FK9873AF475DE907")
      .OnTable("RoomMute");


Delete.ForeignKey("FK9873AF411868503")
      .OnTable("RoomMute");


Delete.ForeignKey("FK2F2D6D4B8005458D")
      .OnTable("RoomData");


Delete.ForeignKey("FK2F2D6D4BE2440413")
      .OnTable("RoomData");


Delete.ForeignKey("FK2F2D6D4BA1F9B824")
      .OnTable("RoomData");


Delete.ForeignKey("FK2F2D6D4BE9D7EC2C")
      .OnTable("RoomData");


Delete.ForeignKey("FK870DFD1CEA5B8F6B")
      .OnTable("RoomCompetitionEntry");


Delete.ForeignKey("FK870DFD1CF8FE1E37")
      .OnTable("RoomCompetitionEntry");


Delete.ForeignKey("FKE2E4B70D94B326B")
      .OnTable("Relationship");


Delete.ForeignKey("FKE2E4B70238A212C")
      .OnTable("Relationship");


Delete.ForeignKey("FK55F7879950AE97E7")
      .OnTable("PollQuestion");


Delete.ForeignKey("FKC2ECA37F8FE1E37")
      .OnTable("Poll");


Delete.ForeignKey("FKA1A08B333E8646A")
      .OnTable("NavigatorCategory");


Delete.ForeignKey("FK4E12B0E811B558E3")
      .OnTable("MoodlightPreset");


Delete.ForeignKey("FK1C0D29333C014DEF")
      .OnTable("MoodlightData");


Delete.ForeignKey("FK5DE3454A7ED30A0B")
      .OnTable("Minimail");


Delete.ForeignKey("FKDB8AFE64B550214F")
      .OnTable("MessengerMessage");


Delete.ForeignKey("FKDB8AFE64FAB03AA0")
      .OnTable("MessengerMessage");


Delete.ForeignKey("FK13647ACAD00CE56F")
      .OnTable("Link");


Delete.ForeignKey("FKDE8AA2F94C9CB897")
      .OnTable("HotelLandingPromos");


Delete.ForeignKey("FKDF85EFA62FE6A4E8")
      .OnTable("HotelLandingManager");


Delete.ForeignKey("FKA7354D6D9DFFE519")
      .OnTable("HallOfFameElement");


Delete.ForeignKey("FK3466FFA86438333")
      .OnTable("GroupForumThread");


Delete.ForeignKey("FK3466FFAB2E044D5")
      .OnTable("GroupForumThread");


Delete.ForeignKey("FK3466FFA5D5B1940")
      .OnTable("GroupForumThread");


Delete.ForeignKey("FK7A67826426F99CBB")
      .OnTable("GroupForumPost");


Delete.ForeignKey("FK7A678264ECC84987")
      .OnTable("GroupForumPost");


Delete.ForeignKey("FK7A678264B2E044D5")
      .OnTable("GroupForumPost");


Delete.ForeignKey("FKFBBA8E27F8FE1E37")
      .OnTable("Group");


Delete.ForeignKey("FKFBBA8E27F428CA4C")
      .OnTable("Group");


Delete.ForeignKey("FKFBBA8E275D5B1940")
      .OnTable("Group");


Delete.ForeignKey("FKFBBA8E273F6DBC9")
      .OnTable("Group");


Delete.ForeignKey("FKFBBA8E2754C3043A")
      .OnTable("Group");


Delete.ForeignKey("FK2357C3F42A90AD8")
      .OnTable("FriendRequest");


Delete.ForeignKey("FK2357C3FB550214F")
      .OnTable("FriendRequest");


Delete.ForeignKey("FK2357C3FFAB03AA0")
      .OnTable("FriendRequest");


Delete.ForeignKey("FK89B740A5321F574B")
      .OnTable("EcotronReward");


Delete.ForeignKey("FK89B740A5F3865CDB")
      .OnTable("EcotronReward");


Delete.ForeignKey("FKB9820A0575DE907")
      .OnTable("ChatMessage");


Delete.ForeignKey("FKB9820A059DFFE519")
      .OnTable("ChatMessage");


Delete.ForeignKey("FK551258103DF7F84B")
      .OnTable("CatalogProduct");


Delete.ForeignKey("FK55125810F8107B18")
      .OnTable("CatalogProduct");


Delete.ForeignKey("FKCFA36A787FC1D4B2")
      .OnTable("CatalogPageLayout");


Delete.ForeignKey("FKCFA36A78F5E88CB8")
      .OnTable("CatalogPageLayout");


Delete.ForeignKey("FKCFA36A782AB945AC")
      .OnTable("CatalogPageLayout");


Delete.ForeignKey("FKCFA36A784C7DC1FC")
      .OnTable("CatalogPageLayout");


Delete.ForeignKey("FKCFA36A784C7DC123")
      .OnTable("CatalogPageLayout");


Delete.ForeignKey("FKCFA36A784C7DC0C2")
      .OnTable("CatalogPageLayout");


Delete.ForeignKey("FKCFA36A784C7DC0E1")
      .OnTable("CatalogPageLayout");


Delete.ForeignKey("FKCFA36A784C7DC080")
      .OnTable("CatalogPageLayout");


Delete.ForeignKey("FKCFA36A784D9405FF")
      .OnTable("CatalogPageLayout");


Delete.ForeignKey("FKCFA36A7824B24313")
      .OnTable("CatalogPageLayout");


Delete.ForeignKey("FKCFA36A785F2BA7BA")
      .OnTable("CatalogPageLayout");


Delete.ForeignKey("FKCFA36A78DFF0BB9B")
      .OnTable("CatalogPageLayout");


Delete.ForeignKey("FKBE955E6E218EC187")
      .OnTable("CatalogPage");


Delete.ForeignKey("FKBE955E6E8C4FC83D")
      .OnTable("CatalogPage");


Delete.ForeignKey("FKBE955E6E91DBDC5F")
      .OnTable("CatalogPage");


Delete.ForeignKey("FKBE955E6ED94EBF50")
      .OnTable("CatalogPage");


Delete.ForeignKey("FK7C1765AF218EC187")
      .OnTable("CatalogOffer");


Delete.ForeignKey("FK5B7D8210EA5B8F6B")
      .OnTable("BaseItem");


Delete.ForeignKey("FK5B7D82104E121F3F")
      .OnTable("BaseItem");


Delete.ForeignKey("FK5B7D8210C92BB699")
      .OnTable("BaseItem");


Delete.ForeignKey("FK5B7D82102AB945AC")
      .OnTable("BaseItem");


Delete.ForeignKey("FK50A53C35E35C297E")
      .OnTable("Badge");


Delete.ForeignKey("FK1E173FAE75ECFEED")
      .OnTable("AvatarEffect");


Delete.ForeignKey("FK8AC9B5B9B51F9CE4")
      .OnTable("AchievementLevel");


Delete.Table("WallItem");


Delete.Table("UserInfo");


Delete.Table("PetItem");


Delete.Table("PetInfo");


Delete.Table("FloorItem");


Delete.Table("BotInfo");


Delete.Table("YoutubeVideo");


Delete.Table("WardrobeItem");


Delete.Table("UserTalent");


Delete.Table("UserSearchLog");


Delete.Table("UserItem");


Delete.Table("UserCaution");


Delete.Table("UserBan");


Delete.Table("UserAchievement");


Delete.Table("TString");


Delete.Table("Translation");


Delete.Table("TradeLock");


Delete.Table("TalentLevel");


Delete.Table("Talent");


Delete.Table("SupportTicket");


Delete.Table("Subscription");


Delete.Table("SongData");


Delete.Table("RoomMute");


Delete.Table("RoomEvent");


Delete.Table("RoomData");


Delete.Table("RoomCompetitionEntry");


Delete.Table("RoomCompetition");


Delete.Table("Relationship");


Delete.Table("PollQuestion");


Delete.Table("Poll");


Delete.Table("NavigatorCategory");


Delete.Table("MoodlightPreset");


Delete.Table("MoodlightData");


Delete.Table("ModerationTemplate");


Delete.Table("Minimail");


Delete.Table("MessengerMessage");


Delete.Table("Link");


Delete.Table("HotelLandingPromos");


Delete.Table("HotelLandingManager");


Delete.Table("HallOfFameElement");


Delete.Table("GroupSymbols");


Delete.Table("GroupSymbolColours");


Delete.Table("GroupForumThread");


Delete.Table("GroupForumPost");


Delete.Table("GroupForum");


Delete.Table("GroupBases");


Delete.Table("GroupBaseColours");


Delete.Table("GroupBackGroundColours");


Delete.Table("Group");


Delete.Table("FriendRequest");


Delete.Table("EcotronReward");


Delete.Table("EcotronLevel");


Delete.Table("ChatMessage");


Delete.Table("CatalogProduct");


Delete.Table("CatalogPageLayout");


Delete.Table("CatalogPage");


Delete.Table("CatalogOffer");


Delete.Table("BaseItem");


Delete.Table("Badge");


Delete.Table("AvatarEffect");


Delete.Table("AchievementLevel");


Delete.Table("Achievement");

		}
	}
}

